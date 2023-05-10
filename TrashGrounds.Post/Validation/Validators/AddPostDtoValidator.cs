using FluentValidation;
using TrashGrounds.Post.Features.Post.AddPost;
using TrashGrounds.Post.gRPC.Services;

namespace TrashGrounds.Post.Validation.Validators;

public class AddPostDtoValidator : AbstractValidator<AddPostEndpoint.AddPostDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public AddPostDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;
        
        RuleFor(dto => dto.Text)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyText)
            .MaximumLength(3000)
            .WithMessage(ValidationMessages.TooLongText);

        RuleFor(dto => dto.AssetId)
            .MustAsync(IsImageNullOrExistsAsync)
            .WithMessage("not exists");
    }
    
    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) => 
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}