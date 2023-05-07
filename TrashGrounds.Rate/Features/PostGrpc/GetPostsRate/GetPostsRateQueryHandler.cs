using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.PostGrpc.GetPostsRate;

public class GetPostsRateQueryHandler : IRequestHandler<GetPostsRateQuery, PostsRateResponse>
{
    private readonly RateDbContext _context;

    public GetPostsRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<PostsRateResponse> Handle(GetPostsRateQuery request, CancellationToken cancellationToken)
    {
        var postsRate = await _context.PostRates
            .Where(rate => request.Ids.Contains(rate.PostId))
            .GroupBy(rate => rate.PostId)
            .Select(g => new PostRateResponse(g.Key, g.Sum(rate => rate.Rate)))
            .ToListAsync(cancellationToken: cancellationToken);

        return new PostsRateResponse(postsRate);
    }
}