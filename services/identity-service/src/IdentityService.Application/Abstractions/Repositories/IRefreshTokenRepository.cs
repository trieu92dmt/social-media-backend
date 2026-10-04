using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(
        RefreshToken refreshToken);

    Task<RefreshToken?>
        GetByTokenAsync(string token);

    Task SaveChangesAsync();
}