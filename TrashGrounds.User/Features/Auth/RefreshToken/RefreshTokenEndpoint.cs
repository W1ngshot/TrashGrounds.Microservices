using TrashGrounds.User.Routing;

namespace TrashGrounds.User.Features.Auth.RefreshToken;

public class RefreshTokenEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/refresh-tokens",
            async (RefreshTokenRequest request, RefreshTokenEndpointHandler handler) =>
                Results.Ok(await handler.Handle(request)));
    }
}