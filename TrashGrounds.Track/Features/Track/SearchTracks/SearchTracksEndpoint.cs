using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public class SearchTracksEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/search",
            async (string? query, Category? category, int take, int? skip, IMediator mediator) =>
                Results.Ok(await mediator.Send(
                    new SearchTracksCommand(query, category, take, skip ?? default))));
    }
}