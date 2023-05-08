using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public class GetTracksFromUserCommandHandler : IRequestHandler<GetTracksFromUserCommand, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserMicroserviceService _userMicroservice;
    private readonly TrackRateService _trackRateService;

    public GetTracksFromUserCommandHandler(
        TrackDbContext context, 
        UserMicroserviceService userMicroservice,
        TrackRateService trackRateService)
    {
        _context = context;
        _userMicroservice = userMicroservice;
        _trackRateService = trackRateService;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(GetTracksFromUserCommand command, CancellationToken cancellationToken)
    {
        var tracks = await _context.MusicTracks
            .Include(track => track.Genres)
            .Where(track => track.UserId == command.UserId && track.Id != command.ExcludeTrackId)
            .ToTracksInfo(command.Take, command.Skip);

        var user = tracks.FirstOrDefault()?.UserId is null ? 
            null : 
            await _userMicroservice.GetUserInfoAsync(tracks.FirstOrDefault()!.UserId);

        var rates = await _trackRateService.GetTracksRate(tracks.Select(track => track.Id));

        return tracks.Select(trackInfo => new FullTrackInfo
        {
            TrackInfo = trackInfo,
            UserInfo = user,
            Rate = rates?.FirstOrDefault(rate => rate.TrackId == trackInfo.Id)
        });
    }
}