using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.DeletePost;

public class DeletePostEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public DeletePostEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{postId:guid}",
                async (Guid postId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeletePostCommand(
                        _userService.GetUserIdOrThrow(),
                        postId))))
            .RequireAuthorization();
    }
}