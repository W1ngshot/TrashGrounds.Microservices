using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksByCategory;

public class GetTracksByCategoryCommandHandler : IRequestHandler<GetTracksByCategoryCommand, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserMicroserviceService _userMicroservice;

    public GetTracksByCategoryCommandHandler(TrackDbContext context, UserMicroserviceService userMicroservice)
    {
        _context = context;
        _userMicroservice = userMicroservice;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(GetTracksByCategoryCommand request, CancellationToken cancellationToken)
    {
        var tracks = _context.MusicTracks.AsNoTracking();

        var orderedTracks = request.Category switch
        {
            Category.New => tracks.OrderByDescending(track => track.UploadDate),
            Category.MostStreaming => tracks.OrderByDescending(track => track.ListensCount),
            Category.Popular => tracks.OrderByDescending(track => track.ListensCount), //TODO Получение с сервиса оценок списка самых популярных треков
            _ => throw new ArgumentOutOfRangeException(nameof(request.Category), request.Category, null)
        };

        var filteredTracks = await orderedTracks
            .Select(track => new TrackInfo
            {
                Id = track.Id,
                Title = track.Title,
                ListensCount = track.ListensCount,
                PictureLink = track.PictureLink,
                UserId = track.UserId
            })
            .Skip(request.Skip)
            .Take(request.TracksCount)
            .ToListAsync(cancellationToken);

        var userIds = filteredTracks.Select(track => track.UserId);
        var users = await _userMicroservice.GetUsersInfoAsync(userIds);

        //TODO Получение оценок
        return filteredTracks.Select(track => new FullTrackInfo
        {
            TrackInfo = track,
            UserInfo = users?.FirstOrDefault(user => user.Id == track.UserId)
        });
    }
}