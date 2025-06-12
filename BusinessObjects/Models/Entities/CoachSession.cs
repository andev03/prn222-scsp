using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class CoachSession : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public Guid CoachId { get; set; }

    public DateTime ScheduledAt { get; set; }

    public string Status { get; set; } = null!;

    public string? SessionNotes { get; set; }
    
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual User Coach { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
