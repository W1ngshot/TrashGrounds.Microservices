using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.DeletePostRate;

public class DeletePostRateEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public DeletePostRateEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("{postId:guid}",
                async ([FromRoute] Guid postId, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new DeletePostRateCommand(_userService.GetUserIdOrThrow(), postId))))
            .RequireAuthorization();
    }
}