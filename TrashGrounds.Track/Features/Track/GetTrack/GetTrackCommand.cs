using MediatR;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public record GetTrackCommand(Guid Id) : IRequest<MusicTrack>;