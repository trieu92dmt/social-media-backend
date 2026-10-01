using BuildingBlocks.Domain.Entities;

namespace IdentityService.Domain.Entities;

public class RolePermission
{
    public Guid RoleId { get; set; } = Guid.Empty;
    public Guid FunctionId { get; set; } = Guid.Empty;
    public Guid UserActionId { get; set; } = Guid.Empty;

    public Role Role { get; set; } = null!;
    public Function Function { get; set; } = null!;
    public UserAction UserAction { get; set; } = null!;
}
