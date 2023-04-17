using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.User.ChangePictureLink;

public class ChangeAvatarLinkEndpoint : IEndpoint
{
    public record ChangeAvatarDto(string NewLink);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/change-avatar",
                async (ChangeAvatarDto dto, IUserService userService, ChangeAvatarLinkEndpointHandler handler) =>
                    Results.Ok(await handler.Handle(
                        new ChangeAvatarLinkRequest(userService.GetUserIdOrThrow(), dto.NewLink))))
            .RequireAuthorization();
    }
}