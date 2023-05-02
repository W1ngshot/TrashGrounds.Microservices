using FluentValidation;
using TrashGrounds.Post.Features.Post.EditPost;

namespace TrashGrounds.Post.Validation.Validators;

public class EditPostDtoValidator : AbstractValidator<EditPostEndpoint.EditPostDto>
{
    public EditPostDtoValidator()
    {
        RuleFor(dto => dto.Text)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyText)
            .MaximumLength(3000)
            .WithMessage(ValidationMessages.TooLongText);
    }
}