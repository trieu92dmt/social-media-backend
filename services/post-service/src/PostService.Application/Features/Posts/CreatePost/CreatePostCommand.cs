namespace PostService.Application.Features.Posts.CreatePost;

public class CreatePostCommand
{
    public string Content { get; set; } = default!;

    public Guid AuthorId { get; set; }
}