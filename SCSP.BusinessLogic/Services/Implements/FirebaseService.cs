using FirebaseAdmin.Auth;
using Microsoft.Extensions.Logging;
using SCSP.BusinessLogic.Services.Interfaces;

namespace SCSP.BusinessLogic.Services.Implements;

public class FirebaseService : IFirebaseService
{
    private readonly ILogger<FirebaseService> _logger;

    public FirebaseService(ILogger<FirebaseService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UserRecord?> CreateUserAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var firebaseUserArgs = new UserRecordArgs()
            {
                Email = email,
                Password = password,
                EmailVerified = false
            };

            return await FirebaseAuth.DefaultInstance.CreateUserAsync(firebaseUserArgs, cancellationToken);
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Firebase authentication error during user creation: {Email}", email);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Firebase user: {Email}", email);
            return null;
        }
    }

    public async Task<FirebaseToken?> VerifyIdTokenAsync(string idToken, CancellationToken cancellationToken = default)
    {
        try
        {
            return await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken, cancellationToken);
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Firebase token verification failed");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token verification");
            return null;
        }
    }

    public async Task<UserRecord?> GetUserAsync(string uid, CancellationToken cancellationToken = default)
    {
        try
        {
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid, cancellationToken);
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Firebase authentication error getting user: {Uid}", uid);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Firebase user: {Uid}", uid);
            return null;
        }
    }

    public async Task<string?> CreateCustomTokenAsync(string uid, CancellationToken cancellationToken = default)
    {
        try
        {
            return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid, cancellationToken);
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Error creating custom token for user: {Uid}", uid);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating custom token for user: {Uid}", uid);
            return null;
        }
    }

    public async Task<bool> DeleteUserAsync(string uid, CancellationToken cancellationToken = default)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
            _logger.LogInformation("User deleted successfully from Firebase: {Uid}", uid);
            return true;
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Firebase authentication error during user deletion: {Uid}", uid);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting Firebase user: {Uid}", uid);
            return false;
        }
    }

    public async Task<bool> DisableUserAsync(string uid, CancellationToken cancellationToken = default)
    {
        try
        {
            var userArgs = new UserRecordArgs()
            {
                Uid = uid,
                Disabled = true
            };

            await FirebaseAuth.DefaultInstance.UpdateUserAsync(userArgs, cancellationToken);
            _logger.LogInformation("User disabled successfully in Firebase: {Uid}", uid);
            return true;
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Firebase authentication error during user disabling: {Uid}", uid);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disabling Firebase user: {Uid}", uid);
            return false;
        }
    }
} 