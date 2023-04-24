using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.AddListen;

public record AddListenCommand(Guid TrackId) : IRequest<SuccessResponse>;