using FluentValidation;
using TrashGrounds.Post.Features.Post.EditPost;
using TrashGrounds.Post.gRPC.Services;

namespace TrashGrounds.Post.Validation.Validators;

public class EditPostDtoValidator : AbstractValidator<EditPostEndpoint.EditPostDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public EditPostDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;
        
        RuleFor(dto => dto.Text)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyText)
            .MaximumLength(3000)
            .WithMessage(ValidationMessages.TooLongText);

        RuleFor(dto => dto.AssetId)
            .MustAsync(IsImageNullOrExistsAsync)
            .When(dto => dto.ChangeAsset)
            .WithMessage("not exists");
    }
    
    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) => 
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}