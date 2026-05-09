using Microsoft.EntityFrameworkCore;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence;

public class PostDbContext : DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
}