using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class MembershipPackage : BaseEntityWithInt
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
