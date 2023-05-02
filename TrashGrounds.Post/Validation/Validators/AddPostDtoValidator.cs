using FluentValidation;
using TrashGrounds.Post.Features.Post.AddPost;

namespace TrashGrounds.Post.Validation.Validators;

public class AddPostDtoValidator : AbstractValidator<AddPostEndpoint.AddPostDto>
{
    public AddPostDtoValidator()
    {
        RuleFor(dto => dto.Text)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyText)
            .MaximumLength(3000)
            .WithMessage(ValidationMessages.TooLongText);
    }
}