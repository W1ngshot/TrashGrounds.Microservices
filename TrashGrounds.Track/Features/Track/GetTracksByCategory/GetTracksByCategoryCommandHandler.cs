using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Features.Track.GetTracksByCategory;

public class GetTracksByCategoryCommandHandler : IRequestHandler<GetTracksByCategoryCommand, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserMicroserviceService _userMicroservice;
    private readonly TrackRateService _trackRateService;

    public GetTracksByCategoryCommandHandler(
        TrackDbContext context, 
        UserMicroserviceService userMicroservice,
        TrackRateService trackRateService)
    {
        _context = context;
        _userMicroservice = userMicroservice;
        _trackRateService = trackRateService;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(GetTracksByCategoryCommand command, CancellationToken cancellationToken)
    {
        return command.Category switch
        {
            Category.New => await GetNewTracks(command.Take, command.Skip),
            Category.MostStreaming => await GetMostStreamingTracks(command.Take, command.Skip),
            Category.Popular => await GetPopularTracks(command.Take, command.Skip),
            _ => throw new ArgumentOutOfRangeException(nameof(command.Category), command.Category, null)
        };
    }

    private async Task<IEnumerable<FullTrackInfo>> GetNewTracks(int take, int skip)
    {
        var tracks = await _context.MusicTracks
            .OrderByDescending(track => track.UploadDate)
            .ToTracksInfo(take, skip);

        return tracks.ToFullTrackInfo(
            await _userMicroservice.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
            await _trackRateService.GetTracksRate(tracks.Select(track => track.Id)));
    }

    private async Task<IEnumerable<FullTrackInfo>> GetMostStreamingTracks(int take, int skip)
    {
        var tracks = await _context.MusicTracks
            .OrderByDescending(track => track.ListensCount)
            .ToTracksInfo(take, skip);
        
        return tracks.ToFullTrackInfo(
            await _userMicroservice.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
            await _trackRateService.GetTracksRate(tracks.Select(track => track.Id)));
    }
    
    private async Task<IEnumerable<FullTrackInfo>> GetPopularTracks(int take, int skip)
    {
        var rates = await _trackRateService.GetBestRatedTracks(take, skip) ?? new List<Rate>();

        var tracks = await _context.MusicTracks
            .Where(track => rates.Select(rate => rate.TrackId).Contains(track.Id))
            .ToTracksInfo(int.MaxValue);

        return tracks.ToFullTrackInfo(
                await _userMicroservice.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
                rates)
            .OrderByDescending(info => info.Rate?.Rating);
    }
}