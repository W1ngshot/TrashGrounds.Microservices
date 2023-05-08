using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public record SearchTracksCommand(
    string? Query, 
    IEnumerable<Guid>? GenresId,
    int Take,
    int Skip) : IRequest<IEnumerable<FullTrackInfo>>;