using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.AddListen;

public class AddListenEndpoint : IEndpoint
{
    public record AddListenDto(Guid TrackId);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        //TODO починить
        endpoints.MapPost("/add-listen", async (AddListenDto dto, IMediator mediator) =>
            Results.Ok(await mediator.Send(new AddListenCommand(dto.TrackId))));
    }
}