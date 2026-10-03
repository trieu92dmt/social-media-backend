using IdentityService.Application
    .Features.Auth.Register;
using IdentityService.Application
    .Features.Auth.Login;
using IdentityService.Application
    .Features.Auth.Refresh;
using IdentityService.Application
    .Features.Auth.Logout;

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
            request.Password,
            request.Phone);

        var userId =
            await _mediator.Send(command);

        return Ok(new
        {
            UserId = userId
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult>
        Login(LoginRequest request)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password);

        var token =
            await _mediator.Send(command);

        return Ok(new
        {
            Token = token
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult>
        Refresh(RefreshRequest request)
    {
        var command =
            new RefreshCommand(
                request.RefreshToken);

        var result =
            await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        LogoutRequest request)
    {
        await _mediator.Send(
            new LogoutCommand(request.RefreshToken));

        return NoContent();
    }
}