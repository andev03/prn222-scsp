using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class User : BaseEntityWithGuid
{
    public string? RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<CoachSession> CoachSessionCoaches { get; set; } = new List<CoachSession>();

    public virtual ICollection<CoachSession> CoachSessionUsers { get; set; } = new List<CoachSession>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();

    public virtual ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<QuitPlan> QuitPlans { get; set; } = new List<QuitPlan>();

    public virtual ICollection<SmokingRecord> SmokingRecords { get; set; } = new List<SmokingRecord>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
}