using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class Notification : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public string NotifType { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime ScheduledTime { get; set; }

    public DateTime? SentAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
