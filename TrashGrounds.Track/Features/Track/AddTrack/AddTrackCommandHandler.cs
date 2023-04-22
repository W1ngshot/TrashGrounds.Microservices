using MediatR;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Models.Main;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackCommandHandler : IRequestHandler<AddTrackCommand, MusicTrack>
{
    private readonly TrackDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddTrackCommandHandler(TrackDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<MusicTrack> Handle(AddTrackCommand request, CancellationToken cancellationToken)
    {
        //TODO запрос на микросервис с файлами и получение ссылок
        //TODO маппинг
        var track = new MusicTrack
        {
            Title = request.Title,
            Description = request.Description,
            IsExplicit = request.IsExplicit,
            ListensCount = 0,
            PictureLink = request.PictureLink,
            MusicLink = request.MusicLink,
            UserId = request.UserId,
            UploadDate = _dateTimeProvider.UtcNow,
            Genres = new List<Models.Main.Genre>()
        };

        foreach (var genreId in request.Genres)
        {
            var genre = await _context.Genres.FindAsync(genreId) 
                ?? throw new NotFoundException<Models.Main.Genre>();
            track.Genres.Add(genre);
        }
        
        _context.MusicTracks.Add(track);
        await _context.SaveEntitiesAsync();

        //TODO Получение пользователя, возможно оценки
        return track;
    }
}