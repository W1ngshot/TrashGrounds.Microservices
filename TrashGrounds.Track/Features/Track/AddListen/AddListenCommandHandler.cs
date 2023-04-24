using MediatR;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.AddListen;

public class AddListenCommandHandler : IRequestHandler<AddListenCommand, SuccessResponse>
{
    private readonly TrackDbContext _context;

    public AddListenCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<SuccessResponse> Handle(AddListenCommand command, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == command.TrackId,
            cancellationToken: cancellationToken);

        track.ListensCount++;

        await _context.SaveEntitiesAsync();

        return new SuccessResponse();
    }
}