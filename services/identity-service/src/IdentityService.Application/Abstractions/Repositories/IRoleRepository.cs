using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByCodeAsync(string code);
}