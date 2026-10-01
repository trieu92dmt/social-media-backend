using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class CommentMediaConfiguration : IEntityTypeConfiguration<CommentMedia>
{
    public void Configure(EntityTypeBuilder<CommentMedia> builder)
    {
        // Ánh xạ entity vào bảng CommentMedia và dùng Id làm khóa chính.
        builder.HasKey(commentMedia => commentMedia.Id);

        // Id dùng kiểu UUID như trong thiết kế.
        builder.Property(commentMedia => commentMedia.Id)
            .HasColumnType("uuid")
            .IsRequired();

        // CommentId và MediaId là các mã UUID bắt buộc.
        builder.Property(commentMedia => commentMedia.CommentId)
            .IsRequired();

        builder.Property(commentMedia => commentMedia.MediaId)
            .IsRequired();

        // Thứ tự hiển thị media là bắt buộc.
        builder.Property(commentMedia => commentMedia.SortOrder)
            .IsRequired();
    }
}