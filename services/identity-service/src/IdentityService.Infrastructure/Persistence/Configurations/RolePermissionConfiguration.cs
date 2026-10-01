using Microsoft.EntityFrameworkCore;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new
        {
            x.RoleId,
            x.FunctionId,
            x.UserActionId
        });

        builder.HasOne(x => x.Role)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Function)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.FunctionId);

        builder.HasOne(x => x.UserAction)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.UserActionId);
    }
}