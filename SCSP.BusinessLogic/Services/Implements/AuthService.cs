using AutoMapper;
using BusinessObjects.Models.Entities;
using Microsoft.Extensions.Logging;
using SCSP.BusinessLogic.DTOs.Commands;
using SCSP.BusinessLogic.DTOs.Queries;
using SCSP.BusinessLogic.Services.Interfaces;
using SCSP.DataAccess.Models;
using SCSP.DataAccess.UnitOfWork;

namespace SCSP.BusinessLogic.Services.Implements;

public class AuthService : IAuthService
{
    private readonly IFirebaseService _firebaseService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(IFirebaseService firebaseService, IUnitOfWork unitOfWork, ILogger<AuthService> logger,
        IMapper mapper)
    {
        _firebaseService = firebaseService ?? throw new ArgumentNullException(nameof(firebaseService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserAccountResponse?> RegisterUserAsync(RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (await UserExistsByEmailAsync(command.Email, cancellationToken))
            {
                _logger.LogWarning("User already exists with email: {Email}", command.Email);
                return null;
            }

            var firebaseUser =
                await _firebaseService.CreateUserAsync(command.Email, command.Password, cancellationToken);

            if (firebaseUser == null)
            {
                _logger.LogWarning("Failed to create Firebase user for email: {Email}", command.Email);
                return null;
            }

            var userAccount = await CreateUserAsync(command.Email, firebaseUser.Uid, cancellationToken);

            if (!string.IsNullOrEmpty(command.FirstName) || !string.IsNullOrEmpty(command.LastName))
            {
                await UpdateUserProfileAsync(userAccount.Guid, command.FirstName, command.LastName, null, null,
                    cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("User registered successfully with email: {Email}", command.Email);

            var userWithProfile = await GetUserWithProfileByGuidAsync(userAccount.Guid, cancellationToken);
            return _mapper.Map<UserAccountResponse>(userWithProfile);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error during user registration: {Email}", command.Email);

            try
            {
                await _firebaseService.DeleteUserAsync(command.Email, cancellationToken);
            }
            catch (Exception cleanupEx)
            {
                _logger.LogError(cleanupEx, "Failed to cleanup Firebase user after registration failure: {Email}",
                    command.Email);
            }

            return null;
        }
    }

    public async Task<UserAccountResponse?> LoginUserAsync(LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var decodedToken = await _firebaseService.VerifyIdTokenAsync(command.IdToken, cancellationToken);

            if (decodedToken == null)
            {
                _logger.LogWarning("Invalid idToken provided");
                return null;
            }

            var userAccount = await GetUserByIdAsync(decodedToken.Uid, cancellationToken);

            if (userAccount == null)
            {
                _logger.LogInformation("User not found in database, creating user for identity ID: {IdentityId}",
                    decodedToken.Uid);

                var firebaseUser = await _firebaseService.GetUserAsync(decodedToken.Uid, cancellationToken);

                if (firebaseUser?.Email == null)
                {
                    _logger.LogError(
                        "Cannot create user in database - no email found in Firebase for identity ID: {IdentityId}",
                        decodedToken.Uid);
                    return null;
                }

                userAccount = await CreateUserAsync(firebaseUser.Email, decodedToken.Uid, cancellationToken);
                _logger.LogInformation("User created automatically in database during login: {Email}",
                    firebaseUser.Email);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("User logged in successfully: {Email}", userAccount.Email);

            var userWithProfile = await GetUserWithProfileByGuidAsync(userAccount.Guid, cancellationToken);
            return _mapper.Map<UserAccountResponse>(userWithProfile);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error during login");
            return null;
        }
    }

    public async Task<bool> VerifyTokenAsync(string idToken, CancellationToken cancellationToken = default)
    {
        try
        {
            var decodedToken = await _firebaseService.VerifyIdTokenAsync(idToken, cancellationToken);
            return decodedToken != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token verification");
            return false;
        }
    }

    public async Task<string?> GetCustomTokenAsync(string identityId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _firebaseService.CreateCustomTokenAsync(identityId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating custom token for user: {IdentityId}", identityId);
            return null;
        }
    }

    public async Task<string?> GetIdentityIdFromTokenAsync(string idToken,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var decodedToken = await _firebaseService.VerifyIdTokenAsync(idToken, cancellationToken);
            return decodedToken?.Uid;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting identity ID from token");
            return null;
        }
    }

    public async Task<bool> DisableUserAsync(string identityId, CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await DeleteUserFromDatabaseAsync(identityId, cancellationToken);

            var firebaseDeleteSuccess = await _firebaseService.DisableUserAsync(identityId, cancellationToken);

            if (!firebaseDeleteSuccess)
            {
                _logger.LogWarning(
                    "Failed to disable user from Firebase, but database disable was successful: {IdentityId}",
                    identityId);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("User disabled: {IdentityId}", identityId);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error disable user completely: {IdentityId}", identityId);
            return false;
        }
    }

    #region Helper

    private async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
        return await userAccountRepo.ExistsAsync(u => u.Email == email, cancellationToken);
    }

    private async Task<UserAccount?> GetUserByIdAsync(string identityId, CancellationToken cancellationToken = default)
    {
        var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
        var users = await userAccountRepo.FindAsync(u => u.IdentityId == identityId, cancellationToken);
        return users.FirstOrDefault();
    }

    private async Task<UserAccount?> GetUserWithProfileByGuidAsync(Guid userGuid,
        CancellationToken cancellationToken = default)
    {
        var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
        var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();

        var user = await userAccountRepo.GetByGuidAsync(userGuid, cancellationToken);
        if (user != null)
        {
            user.UserProfile = await userProfileRepo.GetByGuidAsync(userGuid, cancellationToken);
        }

        return user;
    }

    private async Task<UserAccount> CreateUserAsync(string email, string identityId,
        CancellationToken cancellationToken = default)
    {
        var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
        var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();

        var userAccount = new UserAccount
        {
            Email = email,
            IdentityId = identityId
        };

        var createdUser = await userAccountRepo.AddAsync(userAccount, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userProfile = new UserProfile
        {
            Guid = createdUser.Guid
        };

        await userProfileRepo.AddAsync(userProfile, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User created successfully: {Email}", email);
        return createdUser;
    }

    private async Task UpdateUserProfileAsync(Guid userGuid, string? firstName, string? lastName, DateOnly? dateOfBirth,
        string? bio, CancellationToken cancellationToken = default)
    {
        var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();
        var userProfile = await userProfileRepo.GetByGuidAsync(userGuid, cancellationToken);

        if (userProfile == null)
        {
            throw new InvalidOperationException($"User profile not found for GUID: {userGuid}");
        }

        userProfile.FirstName = firstName;
        userProfile.LastName = lastName;
        userProfile.DateOfBirth = dateOfBirth;
        userProfile.Bio = bio;

        await userProfileRepo.UpdateAsync(userProfile, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User profile updated successfully for GUID: {Guid}", userGuid);
    }

    private async Task DeleteUserFromDatabaseAsync(string identityId, CancellationToken cancellationToken = default)
    {
        var user = await GetUserByIdAsync(identityId, cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException($"User not found for identity ID: {identityId}");
        }

        var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
        var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();

        var userProfile = await userProfileRepo.GetByGuidAsync(user.Guid, cancellationToken);
        if (userProfile != null)
        {
            await userProfileRepo.DeleteByGuidAsync(user.Guid, cancellationToken);
        }

        await userAccountRepo.DeleteByGuidAsync(user.Guid, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User deleted from database: {IdentityId}", identityId);
    }

    #endregion
}