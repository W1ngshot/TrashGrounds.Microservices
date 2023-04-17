using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/login", async (LoginRequest request, LoginEndpointHandler handler) => 
            Results.Ok(await handler.Handle(request)))
            .AddValidation(validation => validation.AddFor<LoginRequest>());
    }
}