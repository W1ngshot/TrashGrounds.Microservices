using MediatR;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public record UpdateTrackCommand(Guid UserId, Guid TrackId, string Title, string? Description,
    bool IsExplicit, string? PictureLink, IEnumerable<Guid> Genres) : IRequest<MusicTrack>;