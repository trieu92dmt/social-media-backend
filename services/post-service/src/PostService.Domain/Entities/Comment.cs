using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class Comment : BaseEntity
{
    public Guid AuthorId { get; set; }
    public string? Content { get; set; }
    public Guid PostId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Post: Many-to-One relationship with Post
    public Post Post { get; set; } = default!;

    // CommentMedia: One-to-Many relationship with CommentMedia
    public ICollection<CommentMedia> CommentMedia { get; set; } = new List<CommentMedia>();

    // UserLikeComment: One-to-Many relationship with UserLikeComment
    public ICollection<UserLikeComment> UserLikeComments { get; set; } = new List<UserLikeComment>();
}
