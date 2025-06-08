namespace SCSP.DataAccess.Models;

public abstract class BaseEntityWithGuid : BaseEntity
{
    public Guid Guid { get; set; }

    protected BaseEntityWithGuid()
    {
        Guid = Guid.NewGuid();
    }
} 