namespace BusinessObjects.Models.Base;

public abstract class BaseEntityWithGuid : BaseEntity
{
    public Guid Guid { get; set; }

    protected BaseEntityWithGuid()
    {
        Guid = Guid.NewGuid();
    }
} 