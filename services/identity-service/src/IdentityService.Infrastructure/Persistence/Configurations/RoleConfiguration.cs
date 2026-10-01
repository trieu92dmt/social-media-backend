using Microsoft.EntityFrameworkCore;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        // Không tự generate ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        // Role Code không được null và có độ dài tối đa là 20 ký tự
        builder.Property(x => x.RoleCode)
            .HasMaxLength(20)
            .IsRequired();

        // Role Name không được null và có độ dài tối đa là 100 ký tự
        builder.Property(x => x.RoleName)
            .HasMaxLength(100)
            .IsRequired();

        // IsActive không được null và có giá trị mặc định là true
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Role Code không được trùng lặp
        builder.HasIndex(x => x.RoleCode)
            .IsUnique();
    }
}