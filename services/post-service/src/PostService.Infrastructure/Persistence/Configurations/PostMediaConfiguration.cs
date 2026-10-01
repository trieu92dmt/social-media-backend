using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
{
    public void Configure(EntityTypeBuilder<PostMedia> builder)
    {
        // Ánh xạ entity vào bảng PostMedia và dùng Id làm khóa chính.
        builder.HasKey(postMedia => postMedia.Id);

        // Id dùng kiểu UUID như trong thiết kế.
        builder.Property(postMedia => postMedia.Id)
            .HasColumnType("uuid")
            .IsRequired();

        // PostId và MediaId là các mã UUID bắt buộc.
        builder.Property(postMedia => postMedia.PostId)
            .IsRequired();

        builder.Property(postMedia => postMedia.MediaId)
            .IsRequired();

        // Thứ tự hiển thị media là bắt buộc.
        builder.Property(postMedia => postMedia.SortOrder)
            .IsRequired();
    }
}