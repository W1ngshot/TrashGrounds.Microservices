using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public record GetTracksFromUserCommand(Guid UserId, Guid? ExcludeTrackId, int TracksCount = 4) 
    : IRequest<IEnumerable<TrackInfo>>;