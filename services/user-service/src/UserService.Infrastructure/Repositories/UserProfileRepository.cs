using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Application.Abstractions.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly UserDbContext _dbContext;

    public UserProfileRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UserProfile userProfile)
    {
        await _dbContext.UserProfiles.AddAsync(userProfile);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserProfile?> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.UserProfiles
            .FirstOrDefaultAsync(up => up.Id == userId);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}