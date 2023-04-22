using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public class GetTracksFromUserEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/from-user/{id:guid}", 
            async (Guid id, int? tracksCount, Guid? excludeTrackId, IMediator mediator) =>
            Results.Ok(await mediator.Send(
                tracksCount is null ? new GetTracksFromUserCommand(id, excludeTrackId) :
                new GetTracksFromUserCommand(id, excludeTrackId, (int) tracksCount))));
    }
}