using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class UserLikePostConfiguration : IEntityTypeConfiguration<UserLikePost>
{
    public void Configure(EntityTypeBuilder<UserLikePost> builder)
    {
        // Sơ đồ dùng UserId và PostId làm khóa chính ghép, không có cột Id riêng.
        builder.Ignore(like => like.Id);
        builder.HasKey(like => new { like.UserId, like.PostId });

        // UserId và PostId là UUID bắt buộc.
        builder.Property(like => like.UserId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(like => like.PostId)
            .HasColumnType("uuid")
            .IsRequired();

        // Ràng buộc UNIQUE riêng trên từng cột theo sơ đồ.
        builder.HasIndex(like => like.UserId)
            .IsUnique();

        builder.HasIndex(like => like.PostId)
            .IsUnique();
    }
}