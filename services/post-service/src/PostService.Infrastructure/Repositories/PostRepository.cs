using PostService.Application.Abstractions.Repositories;
using PostService.Domain.Entities;
using PostService.Infrastructure.Persistence;

namespace PostService.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostDbContext _dbContext;

    public PostRepository(PostDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Post post)
    {
        await _dbContext.Posts.AddAsync(post);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}