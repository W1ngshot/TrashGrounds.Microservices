using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public AddTrackEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public record AddTrackDto(string Title, string? Description,
        bool IsExplicit, string? PictureLink, string MusicLink, IEnumerable<Guid> Genres);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/add", async (AddTrackDto dto, IMediator mediator) =>
            Results.Ok(await mediator.Send(new AddTrackCommand(
                _userService.GetUserIdOrThrow(),
                dto.Title,
                dto.Description,
                dto.IsExplicit,
                dto.PictureLink,
                dto.MusicLink,
                dto.Genres))))
            .RequireAuthorization(); //TODO маппер
        // TODO валидация
    }
}