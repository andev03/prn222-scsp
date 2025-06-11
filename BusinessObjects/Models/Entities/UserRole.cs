using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class UserRole : BaseEntity
{
    public Guid? UserGuid { get; set; }

    public Guid? RoleGuid { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual Role? Role { get; set; }

    public virtual UserAccount? User { get; set; }
}
