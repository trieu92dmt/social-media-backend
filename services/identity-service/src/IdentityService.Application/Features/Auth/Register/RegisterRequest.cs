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

    // ConfirmPassword
    public string ConfirmPassword { get; set; }
        = default!;

    public string Phone { get; set; }
        = default!;
    
    // FullName
    public string FullName { get; set; }
        = default!;

    // Date of Birth
    public DateTime? DOB { get; set; }

    // Address
    public string Address { get; set; }
        = default!;
}