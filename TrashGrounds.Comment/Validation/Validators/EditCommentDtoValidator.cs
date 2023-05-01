using FluentValidation;
using TrashGrounds.Comment.Features.Comment.EditComment;

namespace TrashGrounds.Comment.Validation.Validators;

public class EditCommentDtoValidator : AbstractValidator<EditCommentEndpoint.EditCommentDto>
{
    public EditCommentDtoValidator()
    {
        RuleFor(dto => dto.Message)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyCommentMessage)
            .MaximumLength(300)
            .WithMessage(ValidationMessages.TooLongCommentMessage);
    }
}