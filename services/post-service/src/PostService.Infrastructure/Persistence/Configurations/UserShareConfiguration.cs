using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class UserShareConfiguration : IEntityTypeConfiguration<UserShare>
{
    public void Configure(EntityTypeBuilder<UserShare> builder)
    {
        // Sơ đồ dùng UserId và PostId làm khóa chính ghép, không có cột Id riêng.
        builder.Ignore(share => share.Id);
        builder.HasKey(share => new { share.UserId, share.PostId });

        // UserId và PostId là UUID bắt buộc.
        builder.Property(share => share.UserId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(share => share.PostId)
            .HasColumnType("uuid")
            .IsRequired();

        // Ràng buộc UNIQUE riêng trên từng cột theo sơ đồ.
        builder.HasIndex(share => share.UserId)
            .IsUnique();

        builder.HasIndex(share => share.PostId)
            .IsUnique();
    }
}