namespace SCSP.BusinessLogic.DTOs.Queries;

public class UserAccountResponse
{
    public Guid Guid { get; set; }
    public string Email { get; set; } = null!;
    public string IdentityId { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserProfileResponse? UserProfile { get; set; }
}