namespace SCSP.DataAccess.Models;

public partial class Archievement : BaseEntityWithGuid
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? IconUrl { get; set; }
}
