using MediatR;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Track.GetUserTrackRate;

public record GetTrackUserRateQuery(Guid UserId, Guid TrackId) : IRequest<TrackUserRateResponse>;