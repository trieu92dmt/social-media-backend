using Microsoft.EntityFrameworkCore;
using MediaService.Domain.Entities;

namespace MediaService.Infrastructure.Persistence;

public class MediaDbContext : DbContext
{
    public MediaDbContext(DbContextOptions<MediaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Media> Media => Set<Media>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MediaDbContext).Assembly);
    }
}