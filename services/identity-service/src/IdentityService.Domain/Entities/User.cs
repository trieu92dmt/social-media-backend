using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class User : BaseEntity
{
    // Email
    public string Email { get; set; }
        = default!;

    // Username
    public string Username { get; set; }
        = default!;

    // Password Hash
    public string PasswordHash { get; set; }
        = default!;

    // Phone
    public string Phone { get; set; }
        = default!;

    // Is Active
    public bool IsActive { get; set; }
        = true;

    // Wrong Password Attempt Count
    public int WrongPassCount { get; set; }
        = 0;

    // Last Login
    public DateTime? LastLogin { get; set; }

    // Last Logout
    public DateTime? LastLogout { get; set; }

    // Created At
    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    // Updated At
    public DateTime? UpdatedAt { get; set; }

    // UserRoles Collection
    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
}