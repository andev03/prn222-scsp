namespace SCSP.DataAccess.Models;

public abstract class BaseEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? Disabled { get; set; }

    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        Disabled = false;
    }
} 