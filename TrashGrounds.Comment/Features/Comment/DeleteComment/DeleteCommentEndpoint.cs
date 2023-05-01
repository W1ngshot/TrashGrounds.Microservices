using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Comment.Features.Comment.EditComment;
using TrashGrounds.Comment.Infrastructure.Routing;
using TrashGrounds.Comment.Infrastructure.ValidationSetup;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Features.Comment.DeleteComment;

public class DeleteCommentEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public DeleteCommentEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{commentId:guid}",
                async ([FromRoute] Guid trackId, Guid commentId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeleteCommentCommand(
                        _userService.GetUserIdOrThrow(),
                        trackId,
                        commentId))))
            .RequireAuthorization();
    }
}