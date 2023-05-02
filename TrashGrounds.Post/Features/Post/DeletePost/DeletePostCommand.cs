using MediatR;

namespace TrashGrounds.Post.Features.Post.DeletePost;

public record DeletePostCommand(Guid CurrentUserId, Guid PostId) : IRequest<bool>;