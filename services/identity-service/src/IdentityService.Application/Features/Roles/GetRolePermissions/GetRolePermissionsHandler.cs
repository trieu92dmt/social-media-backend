using IdentityService.Application.Abstractions.Repositories;
using MediatR;

namespace IdentityService.Application
    .Features.Roles.GetRolePermissions;

public class GetRolePermissionsHandler
    : IRequestHandler<GetRolePermissionsCommand, List<string>>
{
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public GetRolePermissionsHandler(
        IRolePermissionRepository rolePermissionRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<List<string>> Handle(
        GetRolePermissionsCommand request,
        CancellationToken cancellationToken)
    {
        var permissions = await _rolePermissionRepository
            .GetPermissionCodesByRoleCodeAsync(request.RoleCode);

        return permissions;
    }
}