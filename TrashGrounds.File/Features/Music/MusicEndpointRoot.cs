using TrashGrounds.Template.Features.Music.GetMusic;
using TrashGrounds.Template.Features.Music.UploadMusic;
using TrashGrounds.Template.Infrastructure.Routing;

namespace TrashGrounds.Template.Features.Music;

public class MusicEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/file/music")
            .WithTags("Музыка")
            .AddEndpoint<UploadMusicEndpoint>()
            .AddEndpoint<GetMusicEndpoint>();
    }
}