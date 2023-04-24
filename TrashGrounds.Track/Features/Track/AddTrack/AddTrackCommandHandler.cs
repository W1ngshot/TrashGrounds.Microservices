using MediatR;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackCommandHandler : IRequestHandler<AddTrackCommand, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UserMicroserviceService _userMicroservice;

    public AddTrackCommandHandler(TrackDbContext context, 
        IDateTimeProvider dateTimeProvider, 
        UserMicroserviceService userMicroservice)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _userMicroservice = userMicroservice;
    }

    public async Task<FullTrack> Handle(AddTrackCommand command, CancellationToken cancellationToken)
    {
        //TODO запрос на микросервис с файлами и получение ссылок
        //TODO маппинг
        var track = new MusicTrack
        {
            Title = command.Title,
            Description = command.Description,
            IsExplicit = command.IsExplicit,
            ListensCount = 0,
            PictureLink = command.PictureLink,
            MusicLink = command.MusicLink,
            UserId = command.UserId,
            UploadDate = _dateTimeProvider.UtcNow,
            Genres = new List<Models.Main.Genre>()
        };

        foreach (var genreId in command.Genres)
        {
            var genre = await _context.Genres.FindAsync(genreId) 
                ?? throw new NotFoundException<Models.Main.Genre>();
            track.Genres.Add(genre);
        }
        
        _context.MusicTracks.Add(track);
        await _context.SaveEntitiesAsync();

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userMicroservice.GetUserInfoAsync(track.UserId)
        };
    }
}