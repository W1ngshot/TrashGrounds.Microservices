using FluentValidation;
using MediatR;
using TrashGrounds.Template.Infrastructure.Routing;
using TrashGrounds.Template.Services.Interfaces;

namespace TrashGrounds.Template.Features.Music.UploadMusic;

public class UploadMusicEndpoint : IEndpoint
{
    public record UploadMusicDto(IFormFile File);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/upload",
                async (IFormFile file, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new UploadMusicCommand(
                        file,
                        userService.GetUserIdOrThrow()))))
            .RequireAuthorization();
        //.AddValidation(builder => builder.AddFor<UploadMusicDto>());
    }

    public class UploadMusicDtoValidator : AbstractValidator<UploadMusicDto>
    {
        public UploadMusicDtoValidator()
        {
            RuleFor(dto => dto.File.Length)
                .GreaterThan(0)
                .WithMessage("Empty file");

            RuleFor(dto => Path.GetExtension(dto.File.FileName))
                .Must(x => new[] {".mp3", ".wav"}.Contains(x))
                .WithMessage("Wrong format");
        }
    }
}