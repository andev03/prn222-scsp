using System.Linq.Expressions;
using BusinessObjects.Models.Base;
using BusinessObjects.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SCSP.DataAccess.DatabaseContexts;

public partial class QuitSmokingAppDbContext : DbContext
{
    public QuitSmokingAppDbContext()
    {
    }

    public QuitSmokingAppDbContext(DbContextOptions<QuitSmokingAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<CoachSession> CoachSessions { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<ForumComment> ForumComments { get; set; }

    public virtual DbSet<ForumPost> ForumPosts { get; set; }

    public virtual DbSet<MembershipPackage> MembershipPackages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<ProgressRecord> ProgressRecords { get; set; }

    public virtual DbSet<QuitPlan> QuitPlans { get; set; }

    public virtual DbSet<SmokingRecord> SmokingRecords { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBadge> UserBadges { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__badges__E79896567224C4D9");

            entity.ToTable("badges", tb => tb.HasTrigger("trg_badges_update"));

            entity.HasIndex(e => e.Code, "UQ__badges__357D4CF99B8C5F9F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("badge_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Criteria)
                .HasMaxLength(255)
                .HasColumnName("criteria");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_mes__0BBF6EE648710871");

            entity.ToTable("chat_messages", tb => tb.HasTrigger("trg_chat_messages_update"));

            entity.Property(e => e.Id).HasColumnName("message_id");
            entity.Property(e => e.MessageText).HasColumnName("message_text");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("sent_at");
            entity.Property(e => e.SessionId).HasColumnName("session_id");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cm_sender");

            entity.HasOne(d => d.Session).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cm_session");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<CoachSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coach_se__69B13FDCC8B1BB4C");

            entity.ToTable("coach_sessions", tb => tb.HasTrigger("trg_coach_sessions_update"));

            entity.Property(e => e.Id).HasColumnName("session_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.ScheduledAt).HasColumnName("scheduled_at");
            entity.Property(e => e.SessionNotes).HasColumnName("session_notes");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachSessionCoaches)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cs_coach");

            entity.HasOne(d => d.User).WithMany(p => p.CoachSessionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cs_user");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__7A6B2B8CCDB62E05");

            entity.ToTable("feedbacks", tb => tb.HasTrigger("trg_feedbacks_update"));

            entity.Property(e => e.Id).HasColumnName("feedback_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fb_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<ForumComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__forum_co__E7957687FEF1FB1E");

            entity.ToTable("forum_comments", tb => tb.HasTrigger("trg_forum_comments_update"));

            entity.Property(e => e.Id).HasColumnName("comment_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comments_posts");

            entity.HasOne(d => d.User).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comments_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<ForumPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__forum_po__3ED787665B91A9CD");

            entity.ToTable("forum_posts", tb => tb.HasTrigger("trg_forum_posts_update"));

            entity.Property(e => e.Id).HasColumnName("post_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ForumPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_posts_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<MembershipPackage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__membersh__63846AE8C932ED08");

            entity.ToTable("membership_packages", tb => tb.HasTrigger("trg_membership_packages_update"));

            entity.Property(e => e.Id).HasColumnName("package_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__notifica__E059842F2BD5710A");

            entity.ToTable("notifications", tb => tb.HasTrigger("trg_notifications_update"));

            entity.Property(e => e.Id).HasColumnName("notification_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.NotifType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notif_type");
            entity.Property(e => e.ScheduledTime).HasColumnName("scheduled_time");
            entity.Property(e => e.SentAt).HasColumnName("sent_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notif_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payments__ED1FC9EA0AE11ACB");

            entity.ToTable("payments", tb => tb.HasTrigger("trg_payments_update"));

            entity.Property(e => e.Id).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pay_subs");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<ProgressRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__progress__49B3D8C1F1B31433");

            entity.ToTable("progress_records", tb => tb.HasTrigger("trg_progress_records_update"));

            entity.Property(e => e.Id).HasColumnName("progress_id");
            entity.Property(e => e.CigarettesSmoked).HasColumnName("cigarettes_smoked");
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(255)
                .HasColumnName("health_status");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");

            entity.HasOne(d => d.Plan).WithMany(p => p.ProgressRecords)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_progress_plan");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<QuitPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__quit_pla__BE9F8F1D3AD7B23A");

            entity.ToTable("quit_plans", tb => tb.HasTrigger("trg_quit_plans_update"));

            entity.Property(e => e.Id).HasColumnName("plan_id");
            entity.Property(e => e.ExpectedEndDate).HasColumnName("expected_end_date");
            entity.Property(e => e.PlanDetails).HasColumnName("plan_details");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.HasOne(d => d.User).WithMany(p => p.QuitPlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_plans_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<SmokingRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__smoking___BFCFB4DDF4BC9534");

            entity.ToTable("smoking_records", tb => tb.HasTrigger("trg_smoking_records_update"));

            entity.Property(e => e.Id).HasColumnName("record_id");
            entity.Property(e => e.CigarettesCount).HasColumnName("cigarettes_count");
            entity.Property(e => e.CostPerPack)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost_per_pack");
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("frequency");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SmokingRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_smokers_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subscrip__863A7EC1EF3658B4");

            entity.ToTable("subscriptions", tb => tb.HasTrigger("trg_subscriptions_update"));

            entity.Property(e => e.Id).HasColumnName("subscription_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Package).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subs_packages");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subs_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__users__B9BE370F1C785580");

            entity.ToTable("users", tb => tb.HasTrigger("trg_users_update"));

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164A01830CD").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC57238890409").IsUnique();

            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        modelBuilder.Entity<UserBadge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_bad__225DFD7220375535");

            entity.ToTable("user_badges", tb => tb.HasTrigger("trg_user_badges_update"));

            entity.Property(e => e.Id).HasColumnName("user_badge_id");
            entity.Property(e => e.AwardedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("awarded_at");
            entity.Property(e => e.BadgeId).HasColumnName("badge_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Badge).WithMany(p => p.UserBadges)
                .HasForeignKey(d => d.BadgeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ub_badges");

            entity.HasOne(d => d.User).WithMany(p => p.UserBadges)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ub_users");

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
        });

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var body = Expression.Equal(
                Expression.Property(parameter, nameof(SoftDeleteEntity.IsDeleted)),
                Expression.Constant(false, typeof(bool?)));

            if (typeof(SoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
            {
                var lambda = Expression.Lambda(body, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}