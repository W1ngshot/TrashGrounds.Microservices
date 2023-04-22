using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public class GetTracksFromUserCommandHandler : IRequestHandler<GetTracksFromUserCommand, IEnumerable<TrackInfo>>
{
    private readonly TrackDbContext _context;

    public GetTracksFromUserCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrackInfo>> Handle(GetTracksFromUserCommand command, CancellationToken cancellationToken)
    {
        var tracks = await _context.MusicTracks
            .Include(track => track.Genres)
            .Where(track => track.UserId == command.UserId && track.Id != command.ExcludeTrackId)
            .Select(track => new TrackInfo
            {
                Id = track.Id,
                Title = track.Title,
                ListensCount = track.ListensCount,
                PictureLink = track.PictureLink,
                UserId = track.UserId
            })
            .Take(command.TracksCount)
            .ToListAsync(cancellationToken);
        
        //TODO запрос на профили пользователей

        return tracks;
    }
}