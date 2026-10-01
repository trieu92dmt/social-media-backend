using FluentValidation;

namespace IdentityService.Application
    .Features.Auth.Login;

public class LoginValidator
    : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}