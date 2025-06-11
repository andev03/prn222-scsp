using FirebaseAdmin.Auth;

namespace SCSP.BusinessLogic.Services.Interfaces;

public interface IFirebaseService
{
    Task<UserRecord?> CreateUserAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<FirebaseToken?> VerifyIdTokenAsync(string idToken, CancellationToken cancellationToken = default);
    Task<UserRecord?> GetUserAsync(string uid, CancellationToken cancellationToken = default);
    Task<string?> CreateCustomTokenAsync(string uid, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(string uid, CancellationToken cancellationToken = default);
    Task<bool> DisableUserAsync(string uid, CancellationToken cancellationToken = default);
} 