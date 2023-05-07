using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;

namespace TrashGrounds.Rate.Features.PostGrpc.GetPostRate;

public class GetPostRateQueryHandler : IRequestHandler<GetPostRateQuery, int>
{
    private readonly RateDbContext _context;

    public GetPostRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetPostRateQuery request, CancellationToken cancellationToken)
    {
        return await _context.PostRates
            .Where(rate => rate.PostId == request.PostId)
            .SumAsync(rate => rate.Rate, cancellationToken: cancellationToken);
    }
}