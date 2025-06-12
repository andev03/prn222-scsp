namespace BusinessObjects.Models.Base;

public abstract class BaseEntityWithGuid : SoftDeleteEntity
{
    public Guid Guid { get; set; }

    protected BaseEntityWithGuid()
    {
        Guid = Guid.NewGuid();
    }
} 