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

        // ConfirmPassword must match Password
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match.");

        // FullName is require
        RuleFor(x => x.FullName)
            .NotEmpty();
    }
}