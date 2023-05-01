using FluentValidation;
using TrashGrounds.Comment.Features.Comment.AddComment;

namespace TrashGrounds.Comment.Validation.Validators;

public class AddCommentDtoValidator : AbstractValidator<AddCommentEndpoint.AddCommentDto>
{
    public AddCommentDtoValidator()
    {
        RuleFor(dto => dto.Message)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyCommentMessage)
            .MaximumLength(300)
            .WithMessage(ValidationMessages.TooLongCommentMessage);
    }
}