using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class CommentMedia : BaseEntity
{
    public Guid CommentId { get; set; }
    public Guid MediaId { get; set; }
    public int SortOrder { get; set; }

    // Comment: Many-to-One relationship with Comment
    public Comment Comment { get; set; } = default!;
}
