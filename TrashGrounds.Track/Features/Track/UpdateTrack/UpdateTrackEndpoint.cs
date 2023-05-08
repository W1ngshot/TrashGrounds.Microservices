using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Infrastructure.ValidationSetup;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public class UpdateTrackEndpoint : IEndpoint
{
    public record UpdateTrackDto(Guid TrackId, string Title, string? Description,
        bool IsExplicit, string? PictureLink, IEnumerable<Guid> Genres);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/update", 
                async (UpdateTrackDto dto, IUserService userService, IMediator mediator) =>
                Results.Ok(await mediator.Send(new UpdateTrackCommand(
                    userService.GetUserIdOrThrow(),
                    dto.TrackId,
                    dto.Title,
                    dto.Description,
                    dto.IsExplicit,
                    dto.PictureLink,
                    dto.Genres))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<UpdateTrackDto>()); //TODO маппер
    }
}