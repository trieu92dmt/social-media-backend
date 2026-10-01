using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class Role : BaseEntity
{
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // UserRoles Collection
    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();

    // RolePermissions Collection
    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();
}
