using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class UserBadge : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public int BadgeId { get; set; }

    public DateTime AwardedAt { get; set; }
    
    public virtual Badge Badge { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
