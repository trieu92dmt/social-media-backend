using BuildingBlocks.Contracts.Posts;
using MassTransit;
using SearchService.Application.Abstractions.Services;
using SearchService.Domain.Documents;

namespace SearchService.Infrastructure.Consumers;

public class PostCreatedConsumer
    : IConsumer<PostCreatedIntegrationEvent>
{
    private readonly ISearchService _searchService;

    public PostCreatedConsumer(ISearchService searchService)
    {
        _searchService = searchService;
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
    }
}