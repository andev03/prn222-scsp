using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class UserBadge : BaseEntity
{
    public Guid? UserGuid { get; set; }

    public Guid? ArchievementGuid { get; set; }

    public DateTime? EarnedAt { get; set; }

    public virtual Archievement? Archievement { get; set; }

    public virtual UserAccount? User { get; set; }
}
