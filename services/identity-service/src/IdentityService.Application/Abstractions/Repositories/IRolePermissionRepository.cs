using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions.Repositories;

public interface IRolePermissionRepository
{
    Task<List<string>> GetPermissionCodesByRoleCodeAsync(string roleCode);
}