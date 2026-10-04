using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByCodeAsync(string code);
}