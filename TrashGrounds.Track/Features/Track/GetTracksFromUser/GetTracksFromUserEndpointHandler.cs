using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public class GetTracksFromUserEndpointHandler : IEndpointHandler<GetTracksFromUserRequest, IEnumerable<Models.Main.Track>>
{
    public Task<IEnumerable<Models.Main.Track>> Handle(GetTracksFromUserRequest request)
    {
        throw new NotImplementedException();
    }
}