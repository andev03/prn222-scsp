namespace SCSP.DataAccess.Models;

public partial class UserAccount : BaseEntityWithGuid
{
    public string Email { get; set; } = null!;

    public string IdentityId { get; set; } = null!;

    public virtual UserProfile? UserProfile { get; set; }
}       
