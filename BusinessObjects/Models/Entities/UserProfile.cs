using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class UserProfile : BaseEntityWithGuid
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Bio { get; set; }

    public virtual UserAccount Gu { get; set; } = null!;
}
