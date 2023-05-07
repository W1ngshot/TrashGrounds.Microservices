using MediatR;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.TrackGrpc.GetTracksRate;

public record GetTracksRateQuery(IEnumerable<Guid> Ids) : IRequest<TracksRateResponse>; 