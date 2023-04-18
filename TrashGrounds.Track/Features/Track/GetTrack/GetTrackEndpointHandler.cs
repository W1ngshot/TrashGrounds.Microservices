using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public class GetTrackEndpointHandler : IEndpointHandler<Guid, Models.Main.Track>
{
    public Task<Models.Main.Track> Handle(Guid request)
    {
        throw new NotImplementedException();
    }
}