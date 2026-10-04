using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task SaveChangesAsync();

    Task<bool> ExistsByEmailAsync(
        string email);

    Task<User?> GetByEmailAsync(
        string email);

    Task<User?> GetByIdAsync(
        Guid id);
}