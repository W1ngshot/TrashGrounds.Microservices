using FluentValidation;
using TrashGrounds.User.Features.User.ChangePassword;
using TrashGrounds.User.Services.Configs;

namespace TrashGrounds.User.Validation.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordEndpoint.ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(dto => dto.OldPassword)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyPassword);

        RuleFor(request => request.NewPassword)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyPassword)
            .MaximumLength(60)
            .WithMessage(ValidationMessages.TooLongPassword)
            .MinimumLength(PasswordConfig.MinimumLength)
            .WithMessage(ValidationMessages.TooShortPassword)
            .Matches(@"^[a-zA-Z0-9\W]+$")
            .WithMessage(ValidationMessages.PasswordContainsWrongSymbols)
            .Matches(@"[a-z]")
            .When(_ => PasswordConfig.RequireLowercase, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationMessages.PasswordRequiresLowercase)
            .Matches(@"[A-Z]")
            .When(_ => PasswordConfig.RequireUppercase, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationMessages.PasswordRequiresUppercase)
            .Matches(@"\d")
            .When(_ => PasswordConfig.RequireDigit, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationMessages.PasswordRequiresDigit)
            .Matches(@"\W")
            .When(_ => PasswordConfig.RequireNonAlphanumeric, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationMessages.PasswordRequiresNonAlphanumeric);
    }
}