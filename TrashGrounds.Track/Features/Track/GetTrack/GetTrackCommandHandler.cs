using MediatR;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public class GetTrackCommandHandler : IRequestHandler<GetTrackCommand, MusicTrack>
{
    private readonly TrackDbContext _context;

    public GetTrackCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<MusicTrack> Handle(GetTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == command.Id, cancellationToken);

        //TODO запрос на профиль пользователя
        
        return track;
    }
}