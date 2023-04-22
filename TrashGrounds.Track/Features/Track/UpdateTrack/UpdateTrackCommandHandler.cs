using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, MusicTrack>
{
    private readonly TrackDbContext _context;

    public UpdateTrackCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<MusicTrack> Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrDefaultAsync(track => track.Id == request.TrackId,
            cancellationToken) ?? throw new NotFoundException<MusicTrack>();

        if (track.UserId != request.UserId)
            throw new ForbiddenException("Can't change not your track");
        
        foreach (var genreId in request.Genres)
        {
            var genre = await _context.Genres.FindAsync(genreId) 
                        ?? throw new NotFoundException<Models.Main.Genre>();
            if (!track.Genres.Contains(genre))
                track.Genres.Add(genre);
        }

        track.Title = request.Title;
        track.Description = request.Description;
        track.IsExplicit = request.IsExplicit;
        track.PictureLink = request.PictureLink; //TODO переписать на вызов сервиса файлов

        await _context.SaveEntitiesAsync();

        //TODO Получение пользователя, возможно оценки
        return track;
    }
}