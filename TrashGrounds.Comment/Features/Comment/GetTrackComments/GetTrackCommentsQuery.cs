using MediatR;
using TrashGrounds.Comment.Models.Additional;

namespace TrashGrounds.Comment.Features.Comment.GetTrackComments;

public record GetTrackCommentsQuery(Guid TrackId, int Take, int Skip) : IRequest<IEnumerable<FullComment>>;