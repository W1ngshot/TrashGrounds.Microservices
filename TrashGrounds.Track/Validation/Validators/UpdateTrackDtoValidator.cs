using FluentValidation;
using TrashGrounds.Track.Features.Track.UpdateTrack;

namespace TrashGrounds.Track.Validation.Validators;

public class UpdateTrackDtoValidator : AbstractValidator<UpdateTrackEndpoint.UpdateTrackDto>
{
    public UpdateTrackDtoValidator()
    {
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
    }
}