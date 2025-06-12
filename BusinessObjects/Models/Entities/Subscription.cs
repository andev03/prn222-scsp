using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class Subscription : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public int PackageId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;
    
    public virtual MembershipPackage Package { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}