using TrashGrounds.User.Features.Auth.AuthCheck;
using TrashGrounds.User.Features.Auth.Login;
using TrashGrounds.User.Features.Auth.RefreshToken;
using TrashGrounds.User.Features.Auth.Register;
using TrashGrounds.User.Routing;

namespace TrashGrounds.User.Features.Auth;

public class AuthEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/auth")
            .WithTags("Авторизация")
            .AddEndpoint<LoginEndpoint>()
            .AddEndpoint<RegisterEndpoint>()
            .AddEndpoint<RefreshTokenEndpoint>()
            .AddEndpoint<AuthCheckEndpoint>();
    }
}