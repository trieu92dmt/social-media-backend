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
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<PostMedia> PostMedia => Set<PostMedia>();
    public DbSet<CommentMedia> CommentMedia => Set<CommentMedia>();
    public DbSet<UserLikePost> UserLikePosts => Set<UserLikePost>();
    public DbSet<UserLikeComment> UserLikeComments => Set<UserLikeComment>();
    public DbSet<UserShare> UserShares => Set<UserShare>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PostDbContext).Assembly);
    }
}