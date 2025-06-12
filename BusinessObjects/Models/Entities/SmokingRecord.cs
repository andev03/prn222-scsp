using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class SmokingRecord : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public DateOnly RecordDate { get; set; }

    public int CigarettesCount { get; set; }

    public string? Frequency { get; set; }

    public decimal? CostPerPack { get; set; }

    public string? Notes { get; set; }
    
    public virtual User User { get; set; } = null!;
}
