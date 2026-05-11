using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
        = default!;

    public string Username { get; set; }
        = default!;

    public string PasswordHash { get; set; }
        = default!;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}