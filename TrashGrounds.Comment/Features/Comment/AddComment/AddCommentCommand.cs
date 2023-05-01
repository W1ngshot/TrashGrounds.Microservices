using MediatR;

namespace TrashGrounds.Comment.Features.Comment.AddComment;

public record AddCommentCommand(
    Guid UserId, 
    Guid TrackId, 
    string Message, 
    Guid? ReplyTo) : IRequest<Models.Main.Comment>;