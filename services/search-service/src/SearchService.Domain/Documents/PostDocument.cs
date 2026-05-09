namespace SearchService.Domain.Documents;

public class PostDocument
{
    public string Id { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}