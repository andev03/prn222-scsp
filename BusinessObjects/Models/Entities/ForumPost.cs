using BusinessObjects.Models.Base;

namespace BusinessObjects.Models.Entities;

public partial class ForumPost : BaseEntityWithInt
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
    
    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();

    public virtual User User { get; set; } = null!;
}
