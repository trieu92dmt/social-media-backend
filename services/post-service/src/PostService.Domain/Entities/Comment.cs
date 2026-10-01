using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class Comment : BaseEntity
{
    public int AuthorId { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }
    public int? ParentCommentId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
