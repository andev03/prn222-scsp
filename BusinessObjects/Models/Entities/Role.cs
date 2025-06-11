using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class Role : BaseEntityWithGuid
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
