using FluentValidation;
using TrashGrounds.User.Features.Auth.Login;

namespace TrashGrounds.User.Validation.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyEmail)
            .EmailAddress()
            .WithMessage(ValidationMessages.IncorrectEmail);

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyPassword);
    }
}