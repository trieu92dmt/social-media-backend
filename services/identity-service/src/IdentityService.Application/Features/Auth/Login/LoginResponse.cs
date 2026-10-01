namespace IdentityService.Application
    .Features.Auth.Login;

public class LoginResponse
{
    public string AccessToken { get; set; }
        = default!;

    public string RefreshToken { get; set; }
        = default!;
}