using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Comment.Infrastructure.Routing;
using TrashGrounds.Comment.Infrastructure.ValidationSetup;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Features.Comment.AddComment;

public class AddCommentEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public AddCommentEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public record AddCommentDto(string Message, Guid? ReplyTo);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/add", async (AddCommentDto dto, [FromRoute] Guid trackId, IMediator mediator) =>
                Results.Ok(await mediator.Send(new AddCommentCommand(
                    _userService.GetUserIdOrThrow(),
                    trackId,
                    dto.Message,
                    dto.ReplyTo))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<AddCommentDto>());
    }
}