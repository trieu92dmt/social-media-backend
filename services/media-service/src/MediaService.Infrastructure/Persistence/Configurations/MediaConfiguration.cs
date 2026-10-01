using MediaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaService.Infrastructure.Persistence.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        // Ánh xạ entity vào bảng Media và dùng Id làm khóa chính.
        builder.HasKey(media => media.Id);

        builder.Property(media => media.Id)
            .HasColumnType("uuid")
            .IsRequired();

        // StorageKey là UUID; các trường metadata đều bắt buộc.
        builder.Property(media => media.StorageKey)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(media => media.MediaType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(media => media.FileName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(media => media.FileSize)
            .IsRequired();

        builder.Property(media => media.CreateTime)
            .IsRequired();
    }
}