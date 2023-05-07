using MediatR;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.TrackGrpc.GetBestRatedTracks;

public record GetBestRatedTracksQuery(int Count) : IRequest<TracksRateResponse>;