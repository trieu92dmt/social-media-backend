using BuildingBlocks.Domain.Entities;

namespace UserService.Domain.Entities;

public class UserFollow
{
    /// <summary>
    /// Người theo dõi (Follower)
    /// </summary>
    public Guid FollowerId { get; set; }
    /// <summary>
    /// Người được theo dõi (Following)
    /// </summary>
    public Guid FollowingId { get; set; }
}
