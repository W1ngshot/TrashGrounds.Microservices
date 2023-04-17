using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public record LoginDto(string Email, string Password);

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/login", async (LoginRequest request, LoginEndpointHandler handler) => 
            Results.Ok(await handler.Handle(request)))
            .AddValidation(builder => builder.AddFor<LoginRequest>());
    }
}