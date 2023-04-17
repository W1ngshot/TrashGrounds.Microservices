using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TrashGrounds.User.Features.Auth.Register;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Services.Configs;

namespace TrashGrounds.User.Validation.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterRequestValidator(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        
        RuleFor(request => request.Nickname)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyNickname)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortNickname)
            .MaximumLength(50)
            .WithMessage(ValidationMessages.TooLongNickname)
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(ValidationMessages.NicknameContainsWrongSymbols)
            .MustAsync(IsUniqueNicknameAsync)
            .WithMessage(ValidationMessages.NicknameAlreadyExists);;

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyEmail)
            .EmailAddress()
            .WithMessage(ValidationMessages.IncorrectEmail)
            .MustAsync(IsUniqueEmailAsync)
            .WithMessage(ValidationMessages.EmailAlreadyExists);

        RuleFor(request => request.Password)
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
    
    private async Task<bool> IsUniqueNicknameAsync(string nickname, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(nickname);
        return user is null;
    }

    private async Task<bool> IsUniqueEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is null;
    }
}