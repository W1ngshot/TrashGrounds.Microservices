using FluentValidation;
using TrashGrounds.User.Features.Auth.Login;

namespace TrashGrounds.User.Validation.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(ValidationMessages.IncorrectEmail);
    }
}