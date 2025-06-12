namespace BusinessObjects.Models.Base;

public abstract class BaseEntityWithInt : SoftDeleteEntity
{
    public int Id { get; set; }
} 