using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence.Configurations;

public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
	public void Configure(EntityTypeBuilder<UserFollow> builder)
	{
		// Cặp người theo dõi và người được theo dõi là khóa chính kép.
		builder.HasKey(follow => new { follow.FollowerId, follow.FollowingId });

		// FollowerId là UUID bắt buộc.
		builder.Property(follow => follow.FollowerId)
			.IsRequired();

		// FollowingId là UUID bắt buộc.
		builder.Property(follow => follow.FollowingId)
			.IsRequired();

		// Không cho phép lưu trùng một cặp follow.
		builder.HasIndex(follow => new { follow.FollowerId, follow.FollowingId })
			.IsUnique();
	}
}
