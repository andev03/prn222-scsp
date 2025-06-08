using SCSP.BusinessLogic.DTOs.Commands;
using SCSP.BusinessLogic.DTOs.Queries;

namespace SCSP.BusinessLogic.Services.Interfaces;

public interface IAuthService
{
    Task<UserAccountResponse?> RegisterUserAsync(RegisterUserCommand command, CancellationToken cancellationToken = default);
    Task<UserAccountResponse?> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken = default);
    Task<bool> VerifyTokenAsync(string idToken, CancellationToken cancellationToken = default);
    Task<string?> GetCustomTokenAsync(string identityId, CancellationToken cancellationToken = default);
    Task<string?> GetIdentityIdFromTokenAsync(string idToken, CancellationToken cancellationToken = default);
    Task<bool> DisableUserAsync(string identityId, CancellationToken cancellationToken = default);
}
