using FluentValidation;
using TrashGrounds.Track.Features.Track.UpdateTrack;
using TrashGrounds.Track.gRPC.Services;

namespace TrashGrounds.Track.Validation.Validators;

public class UpdateTrackDtoValidator : AbstractValidator<UpdateTrackEndpoint.UpdateTrackDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public UpdateTrackDtoValidator(FileExistsService fileExistsService)
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

        RuleFor(dto => dto.TrackId)
            .NotEmpty();

        RuleFor(dto => dto.NewMusicId)
            .NotEmpty()
            .WithMessage("empty")
            .MustAsync(IsMusicExistsAsync)
            .When(dto => dto.ChangeMusic)
            .WithMessage("not exists");
        
        RuleFor(dto => dto.NewPictureId)
            .NotEmpty()
            .WithMessage("empty")
            .MustAsync(IsImageExistsAsync)
            .When(dto => dto.ChangePicture)
            .WithMessage("not exists");;
    }

    private async Task<bool> IsMusicExistsAsync(Guid? trackId, CancellationToken cancellationToken)
    {
        return trackId != null && await _fileExistsService.IsTrackExistsAsync(trackId.Value);
    }

    private async Task<bool> IsImageExistsAsync(Guid? imageId, CancellationToken cancellationToken)
    {
        return imageId != null && await _fileExistsService.IsImageExistsAsync(imageId.Value);
    }
}