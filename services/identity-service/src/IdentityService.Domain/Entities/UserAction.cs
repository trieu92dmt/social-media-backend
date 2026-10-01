using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class UserAction : BaseEntity
{
    public string ActionCode { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // RolePermissions Collection
    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();
}
