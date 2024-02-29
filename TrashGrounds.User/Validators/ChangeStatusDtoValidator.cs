using FluentValidation;
using TrashGrounds.User.Features.ChangeStatus;
using TrashGrounds.User.Infrastructure.Constants;

namespace TrashGrounds.User.Validators;

public class ChangeStatusDtoValidator : AbstractValidator<ChangeStatusEndpoint.ChangeStatusDto>
{
    public ChangeStatusDtoValidator()
    {
        RuleFor(dto => dto.NewStatus)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MinimumLength(5)
            .WithMessage(ValidationFailedMessages.TooShortField)
            .MaximumLength(500)
            .WithMessage(ValidationFailedMessages.TooLongField);
    }
}