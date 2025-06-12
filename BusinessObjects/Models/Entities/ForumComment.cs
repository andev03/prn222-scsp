using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class ForumComment: BaseEntityWithInt
{
    public int PostId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; } = null!;
    
    public virtual ForumPost Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
