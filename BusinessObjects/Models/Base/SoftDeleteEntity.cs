namespace BusinessObjects.Models.Base;

public class SoftDeleteEntity : BaseEntity
{
    public bool? IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public SoftDeleteEntity()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}