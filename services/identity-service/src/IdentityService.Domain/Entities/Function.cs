using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class Function : BaseEntity
{
    public string FunctionCode { get; set; } = string.Empty;
    public string FunctionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // RolePermissions Collection
    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();   
}
