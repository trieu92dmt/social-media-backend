using BuildingBlocks.Domain.Entities;

namespace PostService.Domain.Entities;

public class UserLikePost : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}
