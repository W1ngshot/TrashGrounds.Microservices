using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Track.GetUserTrackRate;

public class GetTrackUserRateEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public GetTrackUserRateEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/",
                async ([FromRoute] Guid trackId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new GetTrackUserRateQuery(
                            _userService.GetUserIdOrThrow(),
                            trackId))))
            .RequireAuthorization();
    }
}