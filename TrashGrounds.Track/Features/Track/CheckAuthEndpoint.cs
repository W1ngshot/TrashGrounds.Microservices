using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track;

public class CheckAuthEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        //TODO удалить
        endpoints.MapGet("/get-current-id",
                ([FromServices] IUserService userService) =>
                    Results.Ok(userService.GetUserIdOrThrow()))
            .RequireAuthorization();
    }
}