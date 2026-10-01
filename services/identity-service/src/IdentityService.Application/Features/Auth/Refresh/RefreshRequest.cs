namespace IdentityService.Application
    .Features.Auth.Refresh;

public class RefreshRequest
{
    public string RefreshToken { get; set; }
        = default!;
}