using IdentityService.Application
    .Features.Auth.Register;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult>
        Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.Email,
            request.Username,
            request.Password);

        var userId =
            await _mediator.Send(command);

        return Ok(new
        {
            UserId = userId
        });
    }
}