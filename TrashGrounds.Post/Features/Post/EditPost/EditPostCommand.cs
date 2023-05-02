using MediatR;

namespace TrashGrounds.Post.Features.Post.EditPost;

public record EditPostCommand(
    Guid CurrentUserId,
    Guid PostId,
    string Text,
    string? AssetLink,
    bool IsHidden) : IRequest<Models.Main.Post>;