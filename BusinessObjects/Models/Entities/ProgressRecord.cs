using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class ProgressRecord : BaseEntityWithInt
{
    public int PlanId { get; set; }

    public DateOnly RecordDate { get; set; }

    public int CigarettesSmoked { get; set; }

    public string? HealthStatus { get; set; }

    public string? Notes { get; set; }
    
    public virtual QuitPlan Plan { get; set; } = null!;
}
