using MediatR;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Infrastructure.ValidationSetup;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.EditPost;

public class EditPostEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public EditPostEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public record EditPostDto(string Text, string? AssetLink, bool IsHidden = false);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{postId:guid}",
                async (EditPostDto dto, Guid postId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new EditPostCommand(
                        _userService.GetUserIdOrThrow(),
                        postId,
                        dto.Text,
                        dto.AssetLink,
                        dto.IsHidden))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<EditPostDto>());
    }
}