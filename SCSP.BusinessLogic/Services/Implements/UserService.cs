using AutoMapper;
using SCSP.BusinessLogic.DTOs.Commands;
using SCSP.BusinessLogic.DTOs.Queries;
using SCSP.BusinessLogic.Services.Interfaces;
using SCSP.DataAccess.Models;
using SCSP.DataAccess.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace SCSP.BusinessLogic.Services.Implements;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserAccountResponse?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
            var users = await userAccountRepo.FindAsync(u => u.Email == email, cancellationToken);
            var user = users.FirstOrDefault();
            
            if (user == null) return null;
            
            var userWithProfile = await GetUserWithProfileByGuidAsync(user.Guid, cancellationToken);
            return _mapper.Map<UserAccountResponse>(userWithProfile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user by email: {Email}", email);
            return null;
        }
    }

    public async Task<UserAccountResponse?> GetUserByIdAsync(string identityId, CancellationToken cancellationToken = default)
    {
        try
        {
            var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
            var users = await userAccountRepo.FindAsync(u => u.IdentityId == identityId, cancellationToken);
            var user = users.FirstOrDefault();
            
            if (user == null) return null;
            
            var userWithProfile = await GetUserWithProfileByGuidAsync(user.Guid, cancellationToken);
            return _mapper.Map<UserAccountResponse>(userWithProfile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user by identity ID: {IdentityId}", identityId);
            return null;
        }
    }

    public async Task<UserAccountResponse?> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        try
        {
            var userWithProfile = await GetUserWithProfileByGuidAsync(guid, cancellationToken);
            return userWithProfile != null ? _mapper.Map<UserAccountResponse>(userWithProfile) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user by GUID: {Guid}", guid);
            return null;
        }
    }

    public async Task<UserAccountResponse> CreateUserAsync(string email, string identityId, CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
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

            await transaction.CommitAsync(cancellationToken);
            
            _logger.LogInformation("User created successfully: {Email}", email);
            
            var userWithProfile = await GetUserWithProfileByGuidAsync(createdUser.Guid, cancellationToken);
            return _mapper.Map<UserAccountResponse>(userWithProfile!);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error creating user: {Email}", email);
            throw;
        }
    }

    public async Task<bool> UpdateUserProfileAsync(UpdateUserProfileCommand command, CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();
            var userProfile = await userProfileRepo.GetByGuidAsync(command.UserGuid, cancellationToken);
            
            if (userProfile == null)
            {
                _logger.LogWarning("User profile not found for GUID: {Guid}", command.UserGuid);
                return false;
            }

            userProfile.FirstName = command.FirstName;
            userProfile.LastName = command.LastName;
            userProfile.DateOfBirth = command.DateOfBirth;
            userProfile.Bio = command.Bio;

            await userProfileRepo.UpdateAsync(userProfile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("User profile updated successfully for GUID: {Guid}", command.UserGuid);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error updating user profile for GUID: {Guid}", command.UserGuid);
            return false;
        }
    }

    public async Task<bool> UploadAvatarAsync(Guid userGuid, string avatarUrl, CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();
            var userProfile = await userProfileRepo.GetByGuidAsync(userGuid, cancellationToken);
            
            if (userProfile == null)
            {
                _logger.LogWarning("User profile not found for GUID: {Guid}", userGuid);
                return false;
            }

            userProfile.AvatarUrl = avatarUrl;
            await userProfileRepo.UpdateAsync(userProfile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Avatar uploaded successfully for user GUID: {Guid}", userGuid);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error uploading avatar for user GUID: {Guid}", userGuid);
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(string identityId, CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await GetUserEntityByIdAsync(identityId, cancellationToken);
            
            if (user == null)
            {
                _logger.LogWarning("User not found for identity ID: {IdentityId}", identityId);
                return false;
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
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("User deleted successfully: {IdentityId}", identityId);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error deleting user: {IdentityId}", identityId);
            return false;
        }
    }

    public async Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
            return await userAccountRepo.ExistsAsync(u => u.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user exists by email: {Email}", email);
            return false;
        }
    }

    public async Task<bool> UserExistsByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default)
    {
        try
        {
            var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
            return await userAccountRepo.ExistsAsync(u => u.IdentityId == identityId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user exists by identity ID: {IdentityId}", identityId);
            return false;
        }
    }

    public async Task<bool> DeleteUserCompletelyAsync(string identityId, CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await GetUserEntityByIdAsync(identityId, cancellationToken);
            
            if (user == null)
            {
                _logger.LogWarning("User not found for identity ID: {IdentityId}", identityId);
                return false;
            }

            var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
            var userProfileRepo = _unitOfWork.GetGuidRepository<UserProfile>();

            var userProfile = await userProfileRepo.GetByGuidAsync(user.Guid, cancellationToken);
            if (userProfile != null)
            {
                await userProfileRepo.HardDeleteByGuidAsync(user.Guid, cancellationToken);
            }

            await userAccountRepo.HardDeleteByGuidAsync(user.Guid, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("User completely deleted: {IdentityId}", identityId);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error completely deleting user: {IdentityId}", identityId);
            return false;
        }
    }

    // Private helper methods for internal entity operations
    private async Task<UserAccount?> GetUserWithProfileByGuidAsync(Guid userGuid, CancellationToken cancellationToken = default)
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

    private async Task<UserAccount?> GetUserEntityByIdAsync(string identityId, CancellationToken cancellationToken = default)
    {
        var userAccountRepo = _unitOfWork.GetGuidRepository<UserAccount>();
        var users = await userAccountRepo.FindAsync(u => u.IdentityId == identityId, cancellationToken);
        return users.FirstOrDefault();
    }
} 