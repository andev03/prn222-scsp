using System;
using System.Collections.Generic;
using BusinessObjects.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SCSP.DataAccess.Models;

public partial class ScspContext : DbContext
{
    public ScspContext()
    {
    }

    public ScspContext(DbContextOptions<ScspContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archievement> Archievements { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserBadge> UserBadges { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archievement>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__archieve__497F6CB4364BA338");

            entity.ToTable("archievement");

            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.IconUrl)
                .HasMaxLength(255)
                .HasColumnName("iconUrl");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasQueryFilter(e => !e.IsDeleted.GetValueOrDefault(false));
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__role__497F6CB4285E5375");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "UQ__role__72E12F1BCCEC1D92").IsUnique();

            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasQueryFilter(e => !e.IsDeleted.GetValueOrDefault(false));
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__user_acc__497F6CB40DDD4716");

            entity.ToTable("user_account");

            entity.HasIndex(e => e.IdentityId, "UQ__user_acc__8A7C0C88DEFA1087").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__user_acc__AB6E6164C701979A").IsUnique();

            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IdentityId)
                .HasMaxLength(255)
                .HasColumnName("identityId");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasQueryFilter(e => !e.IsDeleted.GetValueOrDefault(false));
        });

        modelBuilder.Entity<UserBadge>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_badges");

            entity.HasIndex(e => new { e.UserGuid, e.ArchievementGuid }, "user_badges_index_1").IsUnique();

            entity.Property(e => e.ArchievementGuid).HasColumnName("archievement_guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.EarnedAt).HasColumnName("earned_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserGuid).HasColumnName("user_guid");

            entity.HasOne(d => d.Archievement).WithMany()
                .HasForeignKey(d => d.ArchievementGuid)
                .HasConstraintName("FK__user_badg__archi__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserGuid)
                .HasConstraintName("FK__user_badg__user___5AEE82B9");
            
            entity.HasQueryFilter(e => !e.IsDeleted.GetValueOrDefault(false));
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK__user_pro__497F6CB4EE4A703E");

            entity.ToTable("user_profile");

            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("guid");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatarUrl");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("bio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("firstName");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("lastName");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Gu).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.Guid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_profi__guid__5812160E");
            
            entity.HasQueryFilter(e => !e.IsDeleted.GetValueOrDefault(false));
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_roles");

            entity.HasIndex(e => new { e.UserGuid, e.RoleGuid }, "user_roles_index_0").IsUnique();

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("assigned_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Disabled)
                .HasDefaultValue(false)
                .HasColumnName("disabled");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.RoleGuid).HasColumnName("role_guid");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserGuid).HasColumnName("user_guid");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleGuid)
                .HasConstraintName("FK__user_role__role___59FA5E80");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserGuid)
                .HasConstraintName("FK__user_role__user___59063A47");
            
            entity.HasQueryFilter(e => !e.IsDeleted.GetValueOrDefault(false));
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
