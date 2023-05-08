using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackCommandHandler : ICommandHandler<AddTrackCommand, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UserInfoService _userInfo;

    public AddTrackCommandHandler(TrackDbContext context, 
        IDateTimeProvider dateTimeProvider, 
        UserInfoService userInfo)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _userInfo = userInfo;
    }

    public async Task<FullTrack> Handle(AddTrackCommand request, CancellationToken cancellationToken)
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

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userInfo.GetUserInfoAsync(track.UserId)
        };
    }
}