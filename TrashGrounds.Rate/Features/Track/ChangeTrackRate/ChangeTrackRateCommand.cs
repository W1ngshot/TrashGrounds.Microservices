using MediatR;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Track.ChangeTrackRate;

public record ChangeTrackRateCommand(Guid UserId, Guid TrackId, int NewRate) : IRequest<TrackUserRateResponse>;