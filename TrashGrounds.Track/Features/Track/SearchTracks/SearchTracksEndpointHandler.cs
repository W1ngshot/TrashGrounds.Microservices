using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public class SearchTracksEndpointHandler : IEndpointHandler<SearchTracksRequest, IEnumerable<Models.Main.Track>>
{
    public Task<IEnumerable<Models.Main.Track>> Handle(SearchTracksRequest request)
    {
        throw new NotImplementedException();
    }
}