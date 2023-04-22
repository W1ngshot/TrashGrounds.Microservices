using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public class GetTrackEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetTrackCommand(id))));
    }
}