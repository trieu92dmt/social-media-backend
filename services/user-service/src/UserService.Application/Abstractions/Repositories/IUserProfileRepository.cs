using UserService.Domain.Entities;

namespace UserService.Application.Abstractions.Repositories;

public interface IUserProfileRepository
{
    Task AddAsync(UserProfile userProfile);

    Task<UserProfile?> GetByUserIdAsync(Guid userId);

    Task SaveChangesAsync();
}