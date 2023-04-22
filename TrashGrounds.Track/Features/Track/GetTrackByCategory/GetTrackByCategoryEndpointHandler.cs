using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTrackByCategory;

public class GetTrackByCategoryEndpointHandler : IEndpointHandler<GetTrackByCategoryRequest, Models.Main.MusicTrack>
{
    public Task<Models.Main.MusicTrack> Handle(GetTrackByCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}