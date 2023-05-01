using MediatR;

namespace TrashGrounds.Comment.Features.Comment.EditComment;

public record EditCommentCommand(
    Guid CurrentUserId,
    Guid TrackId,
    Guid CommentId, 
    string Message,
    Guid? ReplyTo) : IRequest<Models.Main.Comment>;