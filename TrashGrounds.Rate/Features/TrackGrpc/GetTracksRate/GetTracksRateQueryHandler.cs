using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.TrackGrpc.GetTracksRate;

public class GetTracksRateQueryHandler : IRequestHandler<GetTracksRateQuery, TracksRateResponse>
{
    private readonly RateDbContext _context;

    public GetTracksRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<TracksRateResponse> Handle(GetTracksRateQuery request, CancellationToken cancellationToken)
    {
        var tracksRate = await _context.TrackRates
            .Where(rate => request.Ids.Contains(rate.TrackId))
            .GroupBy(rate => rate.TrackId)
            .Select(g => new TrackRateResponse(g.Key, g.Average(r => r.Rate)))
            .ToListAsync(cancellationToken: cancellationToken);

        return new TracksRateResponse(tracksRate);
    }
}