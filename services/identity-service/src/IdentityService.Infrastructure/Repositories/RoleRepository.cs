using IdentityService.Application.Abstractions.Repositories;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Repositories;

public class RoleRepository
    : IRoleRepository
{
    private readonly IdentityDbContext _dbContext;

    public RoleRepository(
        IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> GetByCodeAsync(string code)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(r => r.RoleCode == code);
    }
}

   