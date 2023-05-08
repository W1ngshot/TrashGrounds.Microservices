using MediatR;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Infrastructure.ValidationSetup;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.EditPost;

public class EditPostEndpoint : IEndpoint
{
    public record EditPostDto(string Text, string? AssetLink, bool IsHidden = false);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{postId:guid}",
                async (EditPostDto dto, Guid postId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new EditPostCommand(
                        userService.GetUserIdOrThrow(),
                        postId,
                        dto.Text,
                        dto.AssetLink,
                        dto.IsHidden))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<EditPostDto>());
    }
}