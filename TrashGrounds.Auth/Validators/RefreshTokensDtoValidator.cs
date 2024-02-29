using FluentValidation;
using TrashGrounds.Auth.Features.RefreshTokens;
using TrashGrounds.Auth.Infrastructure.Constants;

namespace TrashGrounds.Auth.Validators;

public class RefreshTokensDtoValidator : AbstractValidator<RefreshTokensEndpoint.RefreshTokensDto>
{
    public RefreshTokensDtoValidator()
    {
        RuleFor(dto => dto.Token)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);

        RuleFor(dto => dto.RefreshToken)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);
    }
}