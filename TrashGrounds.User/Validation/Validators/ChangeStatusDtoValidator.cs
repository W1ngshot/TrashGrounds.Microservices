using FluentValidation;
using TrashGrounds.User.Features.User.ChangeStatus;

namespace TrashGrounds.User.Validation.Validators;

public class ChangeStatusDtoValidator : AbstractValidator<ChangeStatusEndpoint.ChangeStatusDto>
{
    public ChangeStatusDtoValidator()
    {
        RuleFor(dto => dto.NewStatus)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyNewStatus)
            .MinimumLength(5)
            .WithMessage(ValidationMessages.TooShortStatus)
            .MaximumLength(300)
            .WithMessage(ValidationMessages.TooLongStatus);
    }
}