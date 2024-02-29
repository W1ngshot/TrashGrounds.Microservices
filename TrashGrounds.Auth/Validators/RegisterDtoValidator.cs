using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TrashGrounds.Auth.Features.Register;
using TrashGrounds.Auth.Infrastructure.Constants;
using TrashGrounds.Auth.Models.Main;
using TrashGrounds.Auth.Services.Configs;

namespace TrashGrounds.Auth.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterEndpoint.RegisterDto>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterDtoValidator(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        
        RuleFor(request => request.Nickname)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MinimumLength(3)
            .WithMessage(ValidationFailedMessages.TooShortField)
            .MaximumLength(50)
            .WithMessage(ValidationFailedMessages.TooLongField)
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(ValidationFailedMessages.WrongSymbols)
            .MustAsync(IsUniqueNicknameAsync)
            .WithMessage(ValidationFailedMessages.AlreadyUsed);;

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .EmailAddress()
            .WithMessage(ValidationFailedMessages.IncorrectEmail)
            .MustAsync(IsUniqueEmailAsync)
            .WithMessage(ValidationFailedMessages.AlreadyUsed);

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MaximumLength(60)
            .WithMessage(ValidationFailedMessages.TooLongField)
            .MinimumLength(PasswordConfig.MinimumLength)
            .WithMessage(ValidationFailedMessages.TooShortField)
            .Matches(@"^[a-zA-Z0-9\W]+$")
            .WithMessage(ValidationFailedMessages.WrongSymbols)
            .Matches(@"[a-z]")
            .When(_ => PasswordConfig.RequireLowercase, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresLowercase)
            .Matches(@"[A-Z]")
            .When(_ => PasswordConfig.RequireUppercase, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresUppercase)
            .Matches(@"\d")
            .When(_ => PasswordConfig.RequireDigit, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresDigit)
            .Matches(@"\W")
            .When(_ => PasswordConfig.RequireNonAlphanumeric, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresNonAlphanumeric);
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