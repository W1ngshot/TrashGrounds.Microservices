using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public class SearchTracksEndpointHandler : IEndpointHandler<SearchTracksRequest, IEnumerable<Models.Main.MusicTrack>>
{
    public Task<IEnumerable<Models.Main.MusicTrack>> Handle(SearchTracksRequest request)
    {
        throw new NotImplementedException();
    }
}