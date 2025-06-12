namespace BusinessObjects.Models.Base;

public abstract class BaseEntity
{
    public bool? Disabled { get; set; }
    
    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    protected BaseEntity()
    {
        Disabled = false;
        CreatedAt = DateTime.Now;
        UpdatedAt = null;
    }
} 