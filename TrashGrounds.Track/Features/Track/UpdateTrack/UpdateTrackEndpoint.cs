using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Infrastructure.ValidationSetup;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public class UpdateTrackEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public UpdateTrackEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public record UpdateTrackDto(Guid TrackId, string Title, string? Description,
        bool IsExplicit, string? PictureLink, IEnumerable<Guid> Genres);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/update", async (UpdateTrackDto dto, IMediator mediator) =>
                Results.Ok(await mediator.Send(new UpdateTrackCommand(
                    _userService.GetUserIdOrThrow(),
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