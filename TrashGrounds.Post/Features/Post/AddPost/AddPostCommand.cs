using MediatR;

namespace TrashGrounds.Post.Features.Post.AddPost;

public record AddPostCommand(Guid UserId, string Text, string? AssetLink, bool IsHidden) : IRequest<Models.Main.Post>;