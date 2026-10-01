using Microsoft.EntityFrameworkCore;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        // Không tự generate ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        // Token không được null và có độ dài tối đa là 100 ký tự
        builder.Property(x => x.Token)
            .HasMaxLength(100)
            .IsRequired();

        // UserId không được null
        builder.Property(x => x.UserId)
            .IsRequired();

        // ExpiresAt không được null
        builder.Property(x => x.ExpiresAt)
            .IsRequired();
    }
}