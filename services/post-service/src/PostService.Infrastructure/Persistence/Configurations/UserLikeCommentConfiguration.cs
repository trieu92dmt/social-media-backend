using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class UserLikeCommentConfiguration : IEntityTypeConfiguration<UserLikeComment>
{
    public void Configure(EntityTypeBuilder<UserLikeComment> builder)
    {
        // Sơ đồ dùng UserId và CommentId làm khóa chính ghép, không có cột Id riêng.
        builder.Ignore(like => like.Id);
        builder.HasKey(like => new { like.UserId, like.CommentId });

        // UserId và CommentId là UUID bắt buộc.
        builder.Property(like => like.UserId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(like => like.CommentId)
            .HasColumnType("uuid")
            .IsRequired();

        // Ràng buộc UNIQUE riêng trên từng cột theo sơ đồ.
        builder.HasIndex(like => like.UserId)
            .IsUnique();

        builder.HasIndex(like => like.CommentId)
            .IsUnique();
    }
}