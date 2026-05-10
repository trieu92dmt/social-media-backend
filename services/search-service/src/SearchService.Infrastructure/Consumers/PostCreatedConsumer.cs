using BuildingBlocks.Contracts.Posts;
using MassTransit;
using SearchService.Application.Abstractions.Services;
using SearchService.Domain.Documents;
using Microsoft.Extensions.Logging;

namespace SearchService.Infrastructure.Consumers;

public class PostCreatedConsumer
    : IConsumer<PostCreatedIntegrationEvent>
{
    private readonly ISearchService _searchService;

    private readonly ILogger<PostCreatedConsumer> _logger;

    public PostCreatedConsumer(ISearchService searchService, ILogger<PostCreatedConsumer> logger)
    {
        _searchService = searchService;
        _logger = logger;
    }

    public async Task Consume(
        ConsumeContext<PostCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var document = new PostDocument
        {
            Id = message.PostId.ToString(),
            Content = message.Content,
            UserId = message.UserId,
            CreatedAt = message.CreatedAt
        };

        await _searchService.IndexPost(document);

        // Log the event
        _logger.LogInformation("Post indexed: {PostId}", document.Id);
    }
}