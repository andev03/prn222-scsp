namespace SCSP.DataAccess.Models;

public partial class Role : BaseEntityWithGuid
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
