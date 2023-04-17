using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features.User.Profile;

public class ProfileEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:Guid}",
            async (Guid id, ProfileEndpointHandler handler) =>
                Results.Ok(await handler.Handle(id)));
    }
}