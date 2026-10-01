using Microsoft.EntityFrameworkCore;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        // Không tự generate ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        // Password không được null và có độ dài tối đa là 100 ký tự
        builder.Property(x => x.PasswordHash)
            .HasMaxLength(100)
            .IsRequired();

        // Email không được null và có độ dài tối đa là 100 ký tự
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        // Phone không được null và có độ dài tối đa là 20 ký tự
        builder.Property(x => x.Phone)
            .HasMaxLength(20)
            .IsRequired();

        // IsActive không được null và có giá trị mặc định là true
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // WrongPassCount không được null và có giá trị mặc định là 0
        builder.Property(x => x.WrongPassCount)
            .IsRequired()
            .HasDefaultValue(0);

        // LastLogin và LastLogout có thể null
        builder.Property(x => x.LastLogin)
            .IsRequired(false);

        // LastLogout có thể null
        builder.Property(x => x.LastLogout)
            .IsRequired(false);

        // CreatedAt không được null
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // UpdatedAt có thể null
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        // Username không được trùng lặp
        builder.HasIndex(x => x.Username)
            .IsUnique();

        // Email không được trùng lặp
        builder.HasIndex(x => x.Email)
            .IsUnique();

        // Phone không được trùng lặp
        builder.HasIndex(x => x.Phone)
            .IsUnique();
    }
}