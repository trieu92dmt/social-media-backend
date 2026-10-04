using IdentityService.Application.Abstractions.Messaging;
using IdentityService.Application.Abstractions.Security;
using IdentityService.Application.Abstractions.Repositories;
using IdentityService.Infrastructure.Messaging;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Security;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection
        AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString(
                        "Postgres"));
            });

        // RabbitMQ connection
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<IdentityDbContext>(o =>
                {
                    o.UsePostgres();
                    o.UseBusOutbox();
                });

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMQ:Username"] ?? "guest");
                    h.Password(configuration["RabbitMQ:Password"] ?? "guest");
                });
            });
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        services.AddScoped<IMessagePublisher, MassTransitMessagePublisher>();

        return services;
    }
}
