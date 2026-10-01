using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
	public void Configure(EntityTypeBuilder<UserProfile> builder)
	{
		// Id là khóa chính của hồ sơ.
		builder.HasKey(profile => profile.Id);

		// Id được cấp từ bên ngoài, không tự sinh.
		builder.Property(profile => profile.Id)
			.ValueGeneratedNever();

		// Tên đầy đủ bắt buộc, tối đa 200 ký tự.
		builder.Property(profile => profile.FullName)
			.HasMaxLength(200)
			.IsRequired();

		// Tên hiển thị không bắt buộc, tối đa 200 ký tự.
		builder.Property(profile => profile.DisplayName)
			.HasMaxLength(200);

		// Địa chỉ không bắt buộc, tối đa 500 ký tự.
		builder.Property(profile => profile.Address)
			.HasMaxLength(500);
	}
}
