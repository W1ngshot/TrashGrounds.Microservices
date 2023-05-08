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
    private readonly TrackRateService _trackRateService;

    public GetTrackCommandHandler(
        TrackDbContext context, 
        UserMicroserviceService userMicroservice,
        TrackRateService trackRateService)
    {
        _context = context;
        _userMicroservice = userMicroservice;
        _trackRateService = trackRateService;
    }

    public async Task<FullTrack> Handle(GetTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == command.Id, cancellationToken);

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userMicroservice.GetUserInfoAsync(track.UserId),
            Rate = await _trackRateService.GetTrackRate(track.Id)
        };
    }
}