using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public class SearchTracksCommandHandler : IRequestHandler<SearchTracksCommand, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserMicroserviceService _userMicroservice;
    private readonly TrackRateService _trackRateService;

    public SearchTracksCommandHandler(
        TrackDbContext context, 
        UserMicroserviceService userMicroservice,
        TrackRateService trackRateService)
    {
        _context = context;
        _userMicroservice = userMicroservice;
        _trackRateService = trackRateService;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(SearchTracksCommand command, CancellationToken cancellationToken)
    {
        var tracks = await _context.MusicTracks
            .Include(track => track.Genres)
            .Where(track =>
                command.Query == null || track.Title.ToLower().Contains(command.Query.ToLower()))
            /*.Where(track => command.GenresId == null || !command.GenresId.Any() ||
                            track.Genres.Select(genre => genre.Id).Intersect(command.GenresId).Count() ==
                            command.GenresId.Count())*/ //TODO не работает, переделать, с ALL тоже не работает
            .ToTracksInfo(command.Take, command.Skip);
        
        return tracks.ToFullTrackInfo(
            await _userMicroservice.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
            await _trackRateService.GetTracksRate(tracks.Select(track => track.Id)));
    }
}