using FluentValidation;
using TrashGrounds.Auth.Features.Login;
using TrashGrounds.Auth.Infrastructure.Constants;

namespace TrashGrounds.Auth.Validators;

public class LoginDtoValidator : AbstractValidator<LoginEndpoint.LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .EmailAddress()
            .WithMessage(ValidationFailedMessages.IncorrectEmail);

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);
    }
}