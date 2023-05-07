using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;

namespace TrashGrounds.Rate.Features.TrackGrpc.GetTrackRate;

public class GetTrackRateQueryHandler : IRequestHandler<GetTrackRateQuery, double>
{
    private readonly RateDbContext _context;

    public GetTrackRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<double> Handle(GetTrackRateQuery request, CancellationToken cancellationToken)
    {
        return await _context.TrackRates.Where(rate => rate.TrackId == request.TrackId)
            .AverageAsync(rate => rate.Rate, cancellationToken: cancellationToken);
    }
}