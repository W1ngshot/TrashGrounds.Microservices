using MediatR;
using TrashGrounds.Template.Infrastructure.Routing;
using TrashGrounds.Template.Infrastructure.ValidationSetup;
using TrashGrounds.Template.Services.Interfaces;

namespace TrashGrounds.Template.Features.Image.UploadImage;

public class UploadImageEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/upload",
                async (IFormFile file, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new UploadImageCommand(
                        file,
                        userService.GetUserIdOrThrow()))))
            .RequireAuthorization();
    }
}