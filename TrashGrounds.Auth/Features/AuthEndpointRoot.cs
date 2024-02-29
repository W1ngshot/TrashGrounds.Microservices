using TrashGrounds.Auth.Features.AuthCheck;
using TrashGrounds.Auth.Features.ChangePassword;
using TrashGrounds.Auth.Features.Login;
using TrashGrounds.Auth.Features.RefreshTokens;
using TrashGrounds.Auth.Features.Register;
using TrashGrounds.Auth.Infrastructure.Routing;

namespace TrashGrounds.Auth.Features;

public class AuthEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/auth")
            .WithTags("Авторизация")
            .AddEndpoint<LoginEndpoint>()
            .AddEndpoint<RegisterEndpoint>()
            .AddEndpoint<RefreshTokensEndpoint>()
            .AddEndpoint<AuthCheckEndpoint>()
            .AddEndpoint<ChangePasswordEndpoint>();
    }
}