using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;

namespace TrashGrounds.Track.Features.Genre.GetAllGenres;

public class GetAllGenresRequestHandler : IRequestHandler<GetAllGenresCommand, IEnumerable<Models.Main.Genre>>
{
    private readonly TrackDbContext _context;

    public GetAllGenresRequestHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Main.Genre>> Handle(GetAllGenresCommand request, CancellationToken cancellationToken)
    {
        return await _context.Genres.AsNoTracking().ToListAsync(cancellationToken);
    }
}