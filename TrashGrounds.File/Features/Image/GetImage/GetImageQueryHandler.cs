using TrashGrounds.Template.Database.Postgres;
using TrashGrounds.Template.Extensions;
using TrashGrounds.Template.Infrastructure;
using TrashGrounds.Template.Infrastructure.Exceptions;
using TrashGrounds.Template.Infrastructure.Mediator.Query;
using TrashGrounds.Template.Models.Additional;
using TrashGrounds.Template.Models.Main;

namespace TrashGrounds.Template.Features.Image.GetImage;

public class GetImageQueryHandler : IQueryHandler<GetImageQuery, FileResponse>
{
    private readonly FileDbContext _context;

    public GetImageQueryHandler(FileDbContext context)
    {
        _context = context;
    }

    public async Task<FileResponse> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var imageFile = await _context.ImageFiles.FirstOrNotFoundAsync(file => file.Id == request.FileId,
            cancellationToken: cancellationToken);

        var filePath = imageFile.Route;

        if (!File.Exists(filePath))
            throw new NotFoundException<MusicFile>();

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory, cancellationToken);
        }
        memory.Position = 0;

        return new FileResponse(memory, ContentTypes.GetType(filePath), imageFile.Id);
    }
}