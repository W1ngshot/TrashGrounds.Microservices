using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public class UpdateTrackEndpointHandler : IEndpointHandler<UpdateTrackRequest, Models.Main.Track>
{
    public Task<Models.Main.Track> Handle(UpdateTrackRequest request)
    {
        throw new NotImplementedException();
    }
}