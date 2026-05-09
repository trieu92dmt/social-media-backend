using MassTransit;
using Nest;
using SearchService.Application.Abstractions.Services;
using SearchService.Infrastructure.Consumers;
using SearchService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SearchService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure Elasticsearch
        services.AddSingleton(provider =>
        {
            var settings = new ConnectionSettings(
                new Uri("http://localhost:9200"))
                .DefaultIndex("posts");

            return new ElasticClient(settings);
        });

        // Register the search service
        services.AddScoped<ISearchService, ElasticsearchService>();

        // Register the consumer
        services.AddMassTransit(config =>
        {
            config.AddConsumer<PostCreatedConsumer>();

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("post-created-search", e =>
                {
                    e.ConfigureConsumer<PostCreatedConsumer>(context);
                });
            });
        });
        return services;
    }
}