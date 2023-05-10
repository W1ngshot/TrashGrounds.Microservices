using TrashGrounds.Template.Database.Postgres;
using TrashGrounds.Template.Extensions;
using TrashGrounds.Template.Infrastructure;
using TrashGrounds.Template.Infrastructure.Exceptions;
using TrashGrounds.Template.Infrastructure.Mediator.Query;
using TrashGrounds.Template.Models.Additional;
using TrashGrounds.Template.Models.Main;

namespace TrashGrounds.Template.Features.Music.GetMusic;

public class GetMusicQueryHandler : IQueryHandler<GetMusicQuery, FileResponse>
{
    private readonly FileDbContext _context;

    public GetMusicQueryHandler(FileDbContext context)
    {
        _context = context;
    }

    public async Task<FileResponse> Handle(GetMusicQuery request, CancellationToken cancellationToken)
    {
        var music = await _context.MusicFiles.FirstOrNotFoundAsync(file => file.Id == request.FileId,
            cancellationToken: cancellationToken);

        var filePath = music.Route;

        if (!File.Exists(filePath))
            throw new NotFoundException<MusicFile>();

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory, cancellationToken);
        }
        memory.Position = 0;

        return new FileResponse(memory, ContentTypes.GetType(filePath), music.Id);
    }
}