using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;

namespace TrashGrounds.User.Features.Auth.Register;

public class RegisterEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/register", async (RegisterRequest request, RegisterEndpointHandler handler) =>
            Results.Ok(await handler.Handle(request)))
            .AddValidation(validation => validation.AddFor<RegisterRequest>());
    }
}