using FluentValidation;
using TrashGrounds.Track.Features.Track.AddTrack;
using TrashGrounds.Track.gRPC.Services;

namespace TrashGrounds.Track.Validation.Validators;

public class AddTrackDtoValidator : AbstractValidator<AddTrackEndpoint.AddTrackDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public AddTrackDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;
        
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyTitle)
            .MinimumLength(2)
            .WithMessage(ValidationMessages.TooShortTitle)
            .MaximumLength(60)
            .WithMessage(ValidationMessages.TooLongTitle)
            .Matches(@"^[\w\s]+$")
            .WithMessage(ValidationMessages.TitleContainsWrongSymbols);

        RuleFor(dto => dto.Description)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortDescription)
            .MaximumLength(300)
            .WithMessage(ValidationMessages.TooLongDescription)
            .Matches(@"^[\w\s]+$")
            .WithMessage(ValidationMessages.DescriptionContainsWrongSymbols);

        RuleFor(dto => dto.Genres)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyGenresList);

        RuleFor(dto => dto.MusicId)
            .NotEmpty()
            .WithMessage("empty music id")
            .MustAsync(IsMusicExistsAsync)
            .WithMessage("not exists");

        RuleFor(dto => dto.PictureId)
            .MustAsync(IsImageNullOrExistsAsync)
            .WithMessage("not exists");
    }
    
    private async Task<bool> IsMusicExistsAsync(Guid trackId, CancellationToken cancellationToken) => 
        await _fileExistsService.IsTrackExistsAsync(trackId);

    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) =>
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}