using TrashGrounds.Template.Database.Postgres;
using TrashGrounds.Template.Infrastructure.Mediator.Command;
using TrashGrounds.Template.Models.Main;
using TrashGrounds.Template.Services.Interfaces;

namespace TrashGrounds.Template.Features.Music.UploadMusic;

public class UploadMusicCommandHandler : ICommandHandler<UploadMusicCommand, Guid>
{
    private readonly FileDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UploadMusicCommandHandler(FileDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(UploadMusicCommand request, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(StoragePaths.MusicPath,
            $"{Guid.NewGuid()}_{_dateTimeProvider.UtcNow.ToShortDateString()}{Path.GetExtension(request.File.FileName)}");
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var musicFile = new MusicFile
        {
            Route = filePath,
            UploadDate = DateTime.UtcNow,
            UserId = request.UserId
        };

        _context.MusicFiles.Add(musicFile);
        await _context.SaveEntitiesAsync();

        return musicFile.Id;
    }
}