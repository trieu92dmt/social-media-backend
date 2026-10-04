using IdentityService.Application.Features.Roles.GetRolePermissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("internal/roles")]
public class InternalRolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InternalRolesController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{roleCode}/permissions")]
    public async Task<IActionResult> GetPermissions(
        string roleCode)
    {
        var permissions =
            await _mediator.Send(new GetRolePermissionsCommand(roleCode));

        return Ok(permissions);
    }
}