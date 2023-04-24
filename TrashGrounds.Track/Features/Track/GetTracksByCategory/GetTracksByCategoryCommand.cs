using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksByCategory;

public record GetTracksByCategoryCommand(Category Category, int Take, int Skip = 0) 
    : IRequest<IEnumerable<FullTrackInfo>>;