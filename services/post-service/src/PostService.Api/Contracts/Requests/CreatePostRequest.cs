namespace PostService.Api.Contracts.Requests;
public class CreatePostRequest
{
    public string Content { get; set; } = default!;
    public Guid AuthorId { get; set; }
}