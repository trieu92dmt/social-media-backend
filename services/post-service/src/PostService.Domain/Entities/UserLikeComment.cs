using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class UserLikeComment : BaseEntity
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }

    // Comment: Many-to-One relationship with Comment
    public Comment Comment { get; set; } = default!;
}
