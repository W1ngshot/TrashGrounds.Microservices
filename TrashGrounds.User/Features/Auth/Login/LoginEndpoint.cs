using TrashGrounds.User.Routing;
using TrashGrounds.User.Validation.Setup;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/login", async (AuthorizationRequest request, LoginEndpointHandler handler) => 
            Results.Ok(await handler.Handle(request)))
            .AddValidation(validation => validation.AddFor<AuthorizationRequest>());
    }
}