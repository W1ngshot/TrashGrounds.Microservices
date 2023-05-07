using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Track.DeleteTrackRate;

public class DeleteTrackRateEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public DeleteTrackRateEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/",
                async ([FromRoute] Guid trackId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new DeleteTrackRateCommand(_userService.GetUserIdOrThrow(), trackId))))
            .RequireAuthorization();
    }
}