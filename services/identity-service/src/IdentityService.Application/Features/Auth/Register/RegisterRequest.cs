namespace IdentityService.Application
    .Features.Auth.Register;

public class RegisterRequest
{
    public string Email { get; set; }
        = default!;

    public string Username { get; set; }
        = default!;

    public string Password { get; set; }
        = default!;
}