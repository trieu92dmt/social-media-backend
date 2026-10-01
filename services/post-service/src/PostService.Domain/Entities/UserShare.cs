using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class UserShare : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    // Post: Many-to-One relationship with Post
    public Post Post { get; set; } = default!;
}
