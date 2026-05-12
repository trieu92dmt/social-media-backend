using FluentValidation;

namespace IdentityService.Application
    .Features.Auth.Register;

public class RegisterValidator
    : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}