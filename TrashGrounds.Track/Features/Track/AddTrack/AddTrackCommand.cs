using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public record AddTrackCommand(Guid UserId, string Title, string? Description,
    bool IsExplicit, string? PictureLink, string MusicLink, IEnumerable<Guid> Genres) : IRequest<FullTrack>;