using IdentityService.Application.Abstractions.Repositories;
using IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly IdentityDbContext _dbContext;

    public RolePermissionRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<string>> GetPermissionCodesByRoleCodeAsync(string roleCode)
    {
        return await _dbContext.RolePermissions
            .Where(rp => rp.Role.RoleCode == roleCode)
            .Select(rp => $"{rp.Function.FunctionCode}.{rp.UserAction.ActionCode}")
            .ToListAsync();
    }
}