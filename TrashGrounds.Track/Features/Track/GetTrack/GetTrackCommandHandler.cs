using MediatR;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public class GetTrackCommandHandler : IRequestHandler<GetTrackCommand, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly UserMicroserviceService _userMicroservice;

    public GetTrackCommandHandler(TrackDbContext context, UserMicroserviceService userMicroservice)
    {
        _context = context;
        _userMicroservice = userMicroservice;
    }

    public async Task<FullTrack> Handle(GetTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == command.Id, cancellationToken);

        //TODO добавить оценку трека в модель
        
        return new FullTrack
        {
            Track = track,
            UserInfo = await _userMicroservice.GetUserInfoAsync(track.UserId)
        };
    }
}