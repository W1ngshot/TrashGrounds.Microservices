using FluentValidation;
using TrashGrounds.User.Features.Auth.Register;

namespace TrashGrounds.User.Validation.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyUsername)
            .MinimumLength(6)
            .WithMessage(ValidationMessages.TooShortUsername)
            .MaximumLength(50)
            .WithMessage(ValidationMessages.TooLongUsername)
            // не работает с ё
            .Matches(@"^[a-zA-Zа-яА-Я]+(\s[a-zA-Zа-яА-Я]+)*$")
            .WithMessage(ValidationMessages.UsernameContainsWrongSymbols);
       
     
    }
}