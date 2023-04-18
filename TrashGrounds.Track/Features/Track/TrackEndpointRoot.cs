using TrashGrounds.Track.Features.Track.AddTrack;
using TrashGrounds.Track.Features.Track.GetTrack;
using TrashGrounds.Track.Features.Track.GetTrackByCategory;
using TrashGrounds.Track.Features.Track.GetTracksFromUser;
using TrashGrounds.Track.Features.Track.SearchTracks;
using TrashGrounds.Track.Features.Track.UpdateTrack;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track;

public class TrackEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/track")
            .WithTags("Треки")
            .AddEndpoint<AddTrackEndpoint>()
            .AddEndpoint<GetTrackEndpoint>()
            .AddEndpoint<GetTrackByCategoryEndpoint>()
            .AddEndpoint<GetTracksFromUserEndpoint>()
            .AddEndpoint<SearchTracksEndpoint>()
            .AddEndpoint<UpdateTrackEndpoint>();
    }
}