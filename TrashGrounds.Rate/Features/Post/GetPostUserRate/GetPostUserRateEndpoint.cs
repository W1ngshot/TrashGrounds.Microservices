using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.GetPostUserRate;

public class GetPostUserRateEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public GetPostUserRateEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("{postId:guid}",
                async ([FromRoute] Guid postId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new GetPostUserRateQuery(
                            _userService.GetUserIdOrThrow(),
                            postId))))
            .RequireAuthorization();
    }
}