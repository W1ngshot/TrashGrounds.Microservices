using TrashGrounds.Template.Features.Image.GetImage;
using TrashGrounds.Template.Features.Image.UploadImage;
using TrashGrounds.Template.Infrastructure.Routing;

namespace TrashGrounds.Template.Features.Image;

public class ImageEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/file/image")
            .WithTags("Изображения")
            .AddEndpoint<UploadImageEndpoint>()
            .AddEndpoint<GetImageEndpoint>();
    }
}