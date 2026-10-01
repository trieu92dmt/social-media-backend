using Microsoft.EntityFrameworkCore;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FunctionConfiguration : IEntityTypeConfiguration<Function>
{
    public void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.HasKey(x => x.Id);

        // Không tự generate ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        // Function Code không được null và có độ dài tối đa là 20 ký tự
        builder.Property(x => x.FunctionCode)
            .HasMaxLength(20)
            .IsRequired();

        // Function Name không được null và có độ dài tối đa là 100 ký tự
        builder.Property(x => x.FunctionName)
            .HasMaxLength(100)
            .IsRequired();

        // IsActive không được null và có giá trị mặc định là true
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Function Code không được trùng lặp
        builder.HasIndex(x => x.FunctionCode)
            .IsUnique();
    }
}