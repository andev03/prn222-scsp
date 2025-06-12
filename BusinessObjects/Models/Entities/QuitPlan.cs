using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class QuitPlan : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public string? Reason { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? ExpectedEndDate { get; set; }

    public string? PlanDetails { get; set; }

    public virtual ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();

    public virtual User User { get; set; } = null!;
}
