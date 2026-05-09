namespace BuildingBlocks.Contracts.Posts;

public class PostCreatedIntegrationEvent
{
    public Guid PostId { get; set; }

    public string Content { get; set; } = default!;

    public string UserId { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
}