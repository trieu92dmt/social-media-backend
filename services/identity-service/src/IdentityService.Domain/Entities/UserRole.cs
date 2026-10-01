using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;
}
