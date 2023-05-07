﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.TrackGrpc.GetBestRatedTracks;

public class GetBestRatedTracksQueryHandler : IRequestHandler<GetBestRatedTracksQuery, TracksRateResponse>
{
    private readonly RateDbContext _context;

    public GetBestRatedTracksQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<TracksRateResponse> Handle(GetBestRatedTracksQuery request, CancellationToken cancellationToken)
    {
        var bestTracks = await _context.TrackRates
            .GroupBy(rate => rate.TrackId)
            .OrderByDescending(g => g.Average(rate => rate.Rate))
            .Take(request.Count)
            .Select(g => new TrackRateResponse(g.Key, g.Average(r => r.Rate)))
            .ToListAsync(cancellationToken: cancellationToken);

        return new TracksRateResponse(bestTracks);
    }
}