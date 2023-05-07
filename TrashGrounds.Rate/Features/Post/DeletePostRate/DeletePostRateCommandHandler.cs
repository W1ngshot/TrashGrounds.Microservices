using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;

namespace TrashGrounds.Rate.Features.Post.DeletePostRate;

public class DeletePostRateCommandHandler : IRequestHandler<DeletePostRateCommand, bool>
{
    private readonly RateDbContext _context;

    public DeletePostRateCommandHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePostRateCommand request, CancellationToken cancellationToken)
    {
        var rate = await _context.PostRates.FirstOrDefaultAsync(
            rate => rate.PostId == request.PostId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        if (rate is null) 
            return true;

        _context.PostRates.Remove(rate);
        await _context.SaveEntitiesAsync();
        return true;
    }
}