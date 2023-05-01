using MediatR;

namespace TrashGrounds.Comment.Features.Comment.DeleteComment;

public record DeleteCommentCommand(Guid CurrentUserId, Guid TrackId, Guid CommentId) : IRequest<bool>;