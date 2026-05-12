using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<bool> ExistsByEmailAsync(
        string email);
}