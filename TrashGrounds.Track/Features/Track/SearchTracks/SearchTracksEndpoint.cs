using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public class SearchTracksEndpoint : IEndpoint
{
    private record SearchDto(IEnumerable<Guid>? Genres)
    {
        public static ValueTask<SearchDto> BindAsync(HttpContext context)
        {
            var queries = context.Request.Query[nameof(Genres)];
            var result = new SearchDto(queries.Select(Guid.Parse!).ToList());
            return ValueTask.FromResult(result);
        }
    }
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/search",
            async (string? query, SearchDto dto, int take, int? skip, IMediator mediator) =>
                Results.Ok(await mediator.Send(
                    new SearchTracksCommand(query, dto.Genres, take, skip ?? default))));
    }
}