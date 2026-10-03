using Microsoft.AspNetCore.Mvc;
using PostService.Api.Contracts.Requests;
using PostService.Application.Features.Posts.CreatePost;

namespace PostService.Api.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly CreatePostHandler _handler;

    public PostsController(CreatePostHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePostRequest request)
    {
        var command = new CreatePostCommand
        {
            Content = request.Content,
            AuthorId = request.AuthorId
        };

        var postId = await _handler.Handle(command);

        return Ok(new
        {
            Id = postId
        });
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test(){
       return Ok(new
        {
            Message = "Post service is running."
        });
    }
}