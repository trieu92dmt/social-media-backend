using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class Post : BaseEntity
{
    public Guid AuthorId { get; set; }
    public string? Content { get; set; }
    // Visibility: Public, Private, FriendsOnly
    public string? Visibility { get; set; }
    // IsActive
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    // UpdatedAt
    public DateTime? UpdatedAt { get; set; }

    // PostMedia: One-to-Many relationship with PostMedia
    public ICollection<PostMedia> PostMedia { get; set; } = new List<PostMedia>();

    // Comments: One-to-Many relationship with Comment
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    // UserLikePost: One-to-Many relationship with UserLikePost
    public ICollection<UserLikePost> UserLikePosts { get; set; } = new List<UserLikePost>();

    // UserShare: One-to-Many relationship with UserShare
    public ICollection<UserShare> UserShares { get; set; } = new List<UserShare>();  
}