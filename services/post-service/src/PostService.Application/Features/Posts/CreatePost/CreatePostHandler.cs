using PostService.Application.Abstractions.Repositories;
using PostService.Domain.Entities;
using MassTransit;
using BuildingBlocks.Contracts.Posts;

namespace PostService.Application.Features.Posts.CreatePost;

public class CreatePostHandler
{
    private readonly IPostRepository _repository;

    private readonly IPublishEndpoint _publishEndpoint;

    public CreatePostHandler(IPostRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreatePostCommand command)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            UserId = command.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(post);

        await _repository.SaveChangesAsync();

        // Publish an event to notify other services that a new post has been created
        await _publishEndpoint.Publish(new PostCreatedIntegrationEvent
        {
            PostId = post.Id,
            Content = post.Content,
            UserId = post.UserId,
            CreatedAt = post.CreatedAt
        });

        return post.Id;
    }
}