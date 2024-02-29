using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Auth.Infrastructure.Routing;
using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Features.AuthCheck;

public class AuthCheckEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/current-id",
                ([FromServices] IUserService userService) => 
                    Results.Ok(userService.GetUserIdOrThrow()))
            .RequireAuthorization();
    }
}