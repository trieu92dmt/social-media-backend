using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class Post : BaseEntity
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; } = default!;
    public string Content { get; set; } = default!;
    // Visibility: Public, Private, FriendsOnly
    public string Visibility { get; set; } = default!;
    // IsActive
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    // UpdatedAt
    public DateTime? UpdatedAt { get; set; }
}