using MediatR;
using TrashGrounds.Template.Infrastructure.Routing;

namespace TrashGrounds.Template.Features.Image.GetImage;

public class GetImageEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}",
            async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetImageQuery(id));
                return Results.File(
                    response.Stream,
                    response.ContentType,
                    response.FileId.ToString());
            });
    }
}