using FluentValidation;
using TrashGrounds.User.Features.Auth.RefreshTokens;

namespace TrashGrounds.User.Validation.Validators;

public class RefreshTokensDtoValidator : AbstractValidator<RefreshTokensEndpoint.RefreshTokensDto>
{
    public RefreshTokensDtoValidator()
    {
        RuleFor(dto => dto.Token)
            .NotEmpty();

        RuleFor(dto => dto.RefreshToken)
            .NotEmpty();
    }
}