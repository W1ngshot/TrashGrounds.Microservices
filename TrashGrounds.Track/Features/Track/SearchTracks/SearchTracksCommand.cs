using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public record SearchTracksCommand(string? Query, Category? Category, int Take, int Skip) : IRequest<IEnumerable<FullTrackInfo>>;