using MediatR;

namespace TrashGrounds.Rate.Features.TrackGrpc.GetTrackRate;

public record GetTrackRateQuery(Guid TrackId) : IRequest<double>;