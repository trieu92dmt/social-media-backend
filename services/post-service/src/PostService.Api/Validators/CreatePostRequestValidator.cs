using FluentValidation;
using PostService.Api.Contracts.Requests;

namespace PostService.Api.Validators;

public class CreatePostRequestValidator
    : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}