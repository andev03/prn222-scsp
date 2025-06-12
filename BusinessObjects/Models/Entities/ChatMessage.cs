using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class ChatMessage: BaseEntityWithInt
{
    public int SessionId { get; set; }

    public Guid SenderId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime SentAt { get; set; }
    
    public virtual User Sender { get; set; } = null!;

    public virtual CoachSession Session { get; set; } = null!;
}
