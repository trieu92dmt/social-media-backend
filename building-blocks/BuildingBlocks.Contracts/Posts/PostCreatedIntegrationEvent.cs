namespace BuildingBlocks.Contracts.Posts;

public class PostCreatedIntegrationEvent
{
    public Guid PostId { get; set; }

    public string Content { get; set; } = default!;

    public Guid AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }
}