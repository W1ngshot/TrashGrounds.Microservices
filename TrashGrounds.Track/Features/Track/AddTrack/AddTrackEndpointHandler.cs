using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackEndpointHandler : IEndpointHandler<AddTrackRequest, Models.Main.Track>
{
    public Task<Models.Main.Track> Handle(AddTrackRequest request)
    {
        throw new NotImplementedException();
    }
}