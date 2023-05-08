using FluentValidation;
using TrashGrounds.User.Features.Auth.Login;

namespace TrashGrounds.User.Validation.Validators;

public class LoginDtoValidator : AbstractValidator<LoginEndpoint.LoginDto>
{
    public LoginDtoValidator()
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