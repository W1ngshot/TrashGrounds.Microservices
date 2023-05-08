using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    private readonly IMapper _mapper;

    public AddTrackCommandHandler(TrackDbContext context, 
        IDateTimeProvider dateTimeProvider, 
        UserInfoService userInfo, 
        IMapper mapper)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _userInfo = userInfo;
        _mapper = mapper;
    }

    public async Task<FullTrack> Handle(AddTrackCommand request, CancellationToken cancellationToken)
    {
        var track = _mapper.Map<AddTrackCommand, MusicTrack>(request);
        track.UploadDate = _dateTimeProvider.UtcNow;
        
        track.Genres = await _context.Genres.Where(genre => request.Genres.Contains(genre.Id))
            .ToListAsync(cancellationToken: cancellationToken);
        if (!track.Genres.Any())
            throw new NotFoundException<Models.Main.Genre>();
        
        //TODO запрос на микросервис с файлами и получение ссылок

        _context.MusicTracks.Add(track);
        await _context.SaveEntitiesAsync();

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userInfo.GetUserInfoAsync(track.UserId),
            Rate = null
        };
    }
}