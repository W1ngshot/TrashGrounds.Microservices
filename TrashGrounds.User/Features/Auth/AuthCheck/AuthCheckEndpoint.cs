using TrashGrounds.User.Routing;

namespace TrashGrounds.User.Features.Auth.AuthCheck;

public class AuthCheckEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/auth-check",
                () => Results.Ok())
            .RequireAuthorization();
    }
}