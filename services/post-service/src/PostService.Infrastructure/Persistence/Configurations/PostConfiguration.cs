using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // Ánh xạ entity vào bảng Post và dùng Id làm khóa chính.
        builder.HasKey(post => post.Id);

        // Id dùng kiểu UUID như trong thiết kế.
        builder.Property(post => post.Id)
            .HasColumnType("uuid")
            .IsRequired();

        // Các thông tin tác giả, nội dung và trạng thái hiển thị là bắt buộc.
        builder.Property(post => post.AuthorId)
            .IsRequired();

        builder.Property(post => post.Content)
            .IsRequired();

        // Visibility tối đa 50 ký tự, ví dụ Public, Friends hoặc Private.
        builder.Property(post => post.Visibility)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(post => post.IsActive)
            .IsRequired();

        builder.Property(post => post.CreatedAt)
            .IsRequired();

        // Thời điểm cập nhật có thể để trống khi bài viết chưa được chỉnh sửa.
        builder.Property(post => post.UpdatedAt)
            .IsRequired(false);
    }
}