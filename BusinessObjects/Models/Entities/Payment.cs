using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class Payment : BaseEntityWithInt
{
    public int SubscriptionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Subscription Subscription { get; set; } = null!;
}
