using MediatR;
using TrashGrounds.Template.Infrastructure.Routing;

namespace TrashGrounds.Template.Features.Music.GetMusic;

public class GetMusicEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}",
            async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetMusicQuery(id));
                return Results.File(
                    response.Stream, 
                    response.ContentType, 
                    response.FileId.ToString(), 
                    null, 
                    null, 
                    true);
            });
    }
}