using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class Badge : BaseEntityWithInt
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Criteria { get; set; }
    
    public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
}
