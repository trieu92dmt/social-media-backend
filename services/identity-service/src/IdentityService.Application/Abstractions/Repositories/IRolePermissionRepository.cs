using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IRolePermissionRepository
{
    Task<List<string>> GetPermissionCodesByRoleCodeAsync(string roleCode);
}