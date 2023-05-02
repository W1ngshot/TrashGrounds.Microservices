using MediatR;
using TrashGrounds.Post.Features.Post.GetUserPosts;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.MyPosts;

public class GetMyPostsEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public GetMyPostsEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/my",
                async (int take, int? skip, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new GetUserPostsQuery(_userService.GetUserIdOrThrow(), take, skip ?? 0, true))))
            .RequireAuthorization();
    }
}