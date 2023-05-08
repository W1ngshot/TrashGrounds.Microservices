using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Infrastructure.ValidationSetup;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackEndpoint : IEndpoint
{
    public record AddTrackDto(string Title, string? Description,
        bool IsExplicit, string? PictureLink, string MusicLink, IEnumerable<Guid> Genres);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/add", async (AddTrackDto dto, IUserService userService, IMediator mediator) =>
                Results.Ok(await mediator.Send(new AddTrackCommand(
                    userService.GetUserIdOrThrow(),
                    dto.Title,
                    dto.Description,
                    dto.IsExplicit,
                    dto.PictureLink,
                    dto.MusicLink,
                    dto.Genres))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<AddTrackDto>()); //TODO маппер
    }
}