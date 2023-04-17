using TrashGrounds.User.Features.User.Profile;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.User.Me;

public class MeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/me",
            async (IUserService userService, ProfileEndpointHandler handler) =>
                Results.Ok(
                    await handler.Handle(
                        userService.GetUserIdOrThrow())));
    }
}