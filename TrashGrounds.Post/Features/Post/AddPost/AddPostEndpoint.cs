using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Infrastructure.ValidationSetup;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.AddPost;

public class AddPostEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public AddPostEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public record AddPostDto(string Text, string? AssetLink, bool IsHidden = false);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/add", async (AddPostDto dto, IMediator mediator) =>
                Results.Ok(await mediator.Send(new AddPostCommand(
                    _userService.GetUserIdOrThrow(),
                    dto.Text,
                    dto.AssetLink,
                    dto.IsHidden))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<AddPostDto>());
    }
}