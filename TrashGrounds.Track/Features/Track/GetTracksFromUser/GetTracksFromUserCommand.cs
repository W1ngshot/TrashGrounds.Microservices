using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public record GetTracksFromUserCommand(Guid UserId, Guid? ExcludeTrackId, int Take, int Skip) 
    : IRequest<IEnumerable<FullTrackInfo>>;