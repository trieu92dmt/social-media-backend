using MediatR;

namespace IdentityService.Application
    .Features.Roles.GetRolePermissions;

public record GetRolePermissionsCommand(
    string RoleCode
) : IRequest<List<string>>;