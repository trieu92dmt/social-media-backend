using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Ánh xạ entity vào bảng Comment và dùng Id làm khóa chính.
        builder.HasKey(comment => comment.Id);

        // Các mã định danh dùng kiểu UUID như trong thiết kế.
        builder.Property(comment => comment.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(comment => comment.AuthorId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(comment => comment.PostId)
            .HasColumnType("uuid")
            .IsRequired();

        // Nội dung bình luận là bắt buộc.
        builder.Property(comment => comment.Content)
            .IsRequired();

        // Bình luận cha không bắt buộc; giá trị mặc định là NULL.
        builder.Property(comment => comment.ParentCommentId)
            .HasColumnType("uuid")
            .IsRequired(false);

        builder.Property(comment => comment.IsActive)
            .IsRequired();

        // Giữ tên cột theo schema trong khi dùng tên thuộc tính theo domain.
        builder.Property(comment => comment.CreatedAt)
            .HasColumnName("CreateTime")
            .IsRequired();

        builder.Property(comment => comment.UpdatedAt)
            .HasColumnName("LastEditTime")
            .IsRequired(false);
    }
}