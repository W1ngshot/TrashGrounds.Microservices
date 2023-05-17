using Microsoft.EntityFrameworkCore;
using TrashGrounds.File.Database.Postgres;
using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Hangfire;

public class GarbageCollectorService
{
    private readonly FileDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GarbageCollectorService(FileDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task DeleteUnusedTracks()
    {
        var usedIds = new List<Guid>(); //grpc to track ms
        var currentDate = _dateTimeProvider.UtcNow;

        var notUsedTracks = await _context.MusicFiles
            .Where(music => (currentDate - music.UploadDate).TotalDays >= 1 && !usedIds.Contains(music.Id))
            .Select(music => new {music.Id, music.Route})
            .ToListAsync();

        _context.Remove(notUsedTracks);
        await _context.SaveEntitiesAsync();

        RemoveUnusedFiles(notUsedTracks.Select(x => x.Route));
    }

    public async Task DeleteUnusedImages()
    {
        var usedIds = new List<Guid>(); //grpc to track ms
        var currentDate = _dateTimeProvider.UtcNow;

        var notUsedImages = await _context.ImageFiles
            .Where(image => (currentDate - image.UploadDate).TotalDays >= 1 && !usedIds.Contains(image.Id))
            .Select(image => new {image.Id, image.Route})
            .ToListAsync();

        _context.Remove(notUsedImages);
        await _context.SaveEntitiesAsync();

        RemoveUnusedFiles(notUsedImages.Select(x => x.Route));
    }

    private static void RemoveUnusedFiles(IEnumerable<string> routes)
    {
        foreach (var route in routes.Where(System.IO.File.Exists))
        {
            try
            {
                System.IO.File.Delete(route);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}