using MediatR;

namespace TrashGrounds.Rate.Features.Track.DeleteTrackRate;

public record DeleteTrackRateCommand(Guid UserId, Guid TrackId) : IRequest<bool>;