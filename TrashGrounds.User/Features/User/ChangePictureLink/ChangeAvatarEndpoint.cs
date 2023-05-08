using MediatR;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.User.ChangePictureLink;

public class ChangeAvatarEndpoint : IEndpoint
{
    public record ChangeAvatarDto(string NewLink);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("/change-avatar",
                async (ChangeAvatarDto dto, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new ChangeAvatarCommand(userService.GetUserIdOrThrow(), dto.NewLink))))
            .RequireAuthorization();
    }
}