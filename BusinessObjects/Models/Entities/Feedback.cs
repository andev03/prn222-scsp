using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class Feedback : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }
    
    public virtual User User { get; set; } = null!;
}
