using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ApiGateway.Api.Authorization;

public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAuthorizationRequirement requirement)
    {
        // Lấy HttpContext từ Authorization context
        if (context.Resource is not HttpContext httpContext)
        {
            return Task.CompletedTask;
        }

        // Lấy endpoint hiện tại
        var endpoint = httpContext.GetEndpoint();

        if (endpoint is null)
        {
            return Task.CompletedTask;
        }

        // Lấy http method của request hiện tại
        var httpMethod = httpContext.Request.Method;

        // Lấy route pattern của endpoint hiện tại
        var actionDescriptor = endpoint.Metadata
            .GetMetadata<ControllerActionDescriptor>();

        var controllerName = actionDescriptor?.ControllerName;
        var actionName = actionDescriptor?.ActionName;

        Console.WriteLine($"Controller: {controllerName}, Action: {actionName}, HTTP Method: {httpMethod}");

        // TEST: Luôn cho phép Requirement
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}