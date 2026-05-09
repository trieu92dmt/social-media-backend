namespace PostService.Api.Contracts.Requests;
public class CreatePostRequest
{
    public string Content { get; set; } = default!;
    public string UserId { get; set; } = default!;
}