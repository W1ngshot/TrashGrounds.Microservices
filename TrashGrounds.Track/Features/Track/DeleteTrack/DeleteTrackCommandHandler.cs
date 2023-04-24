using MediatR;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.DeleteTrack;

public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, SuccessResponse>
{
    private readonly TrackDbContext _context;

    public DeleteTrackCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<SuccessResponse> Handle(DeleteTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == command.TrackId,
            cancellationToken);
        
        if (track.UserId != command.UserId)
            throw new ForbiddenException("Can't delete not your track");
        
        //TODO Удаление из File микросервиса

        _context.MusicTracks.Remove(track);
        await _context.SaveEntitiesAsync();

        return new SuccessResponse("Deleted");
    }
}