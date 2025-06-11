using SCSP.BusinessLogic.DTOs.Commands;
using SCSP.BusinessLogic.DTOs.Queries;

namespace SCSP.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    Task<UserAccountResponse?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<UserAccountResponse?> GetUserByIdAsync(string identityId, CancellationToken cancellationToken = default);
    Task<UserAccountResponse?> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
    Task<UserAccountResponse> CreateUserAsync(string email, string identityId, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserProfileAsync(UpdateUserProfileCommand command, CancellationToken cancellationToken = default);
    Task<bool> UploadAvatarAsync(Guid userGuid, string avatarUrl, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserAsync(string identityId, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserCompletelyAsync(string identityId, CancellationToken cancellationToken = default);
    Task<bool> UserExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> UserExistsByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);
}