using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.DeleteTrack;

public class DeleteTrackEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public DeleteTrackEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{trackId:guid}", async (Guid trackId, IMediator mediator) =>
                Results.Ok(await mediator.Send(
                    new DeleteTrackCommand(_userService.GetUserIdOrThrow(), trackId))))
            .RequireAuthorization();
    }
}