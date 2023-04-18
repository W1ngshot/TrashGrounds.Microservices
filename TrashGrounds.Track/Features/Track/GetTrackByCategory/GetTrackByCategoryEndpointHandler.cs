using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTrackByCategory;

public class GetTrackByCategoryEndpointHandler : IEndpointHandler<GetTrackByCategoryRequest, Models.Main.Track>
{
    public Task<Models.Main.Track> Handle(GetTrackByCategoryRequest request)
    {
        throw new NotImplementedException();
    }
}