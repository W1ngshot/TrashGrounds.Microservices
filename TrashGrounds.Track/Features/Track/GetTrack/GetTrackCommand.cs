using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public record GetTrackCommand(Guid Id) : IRequest<FullTrack>;