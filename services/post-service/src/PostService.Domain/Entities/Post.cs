namespace PostService.Domain.Entities;

public class Post
{
    public Guid Id { get; set; }

    public string Content { get; set; } = default!;

    public string UserId { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
}