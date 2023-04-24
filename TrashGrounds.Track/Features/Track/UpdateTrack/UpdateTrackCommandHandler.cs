using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly UserMicroserviceService _userMicroservice;

    public UpdateTrackCommandHandler(TrackDbContext context, UserMicroserviceService userMicroservice)
    {
        _context = context;
        _userMicroservice = userMicroservice;
    }

    public async Task<FullTrack> Handle(UpdateTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks
            .Include(track => track.Genres)
            .FirstOrNotFoundAsync(track => track.Id == command.TrackId, cancellationToken);

        if (track.UserId != command.UserId)
            throw new ForbiddenException("Can't change not your track");
        
        foreach (var genreId in command.Genres)
        {
            var genre = await _context.Genres.FindAsync(genreId) 
                        ?? throw new NotFoundException<Models.Main.Genre>();
            if (!track.Genres.Contains(genre))
                track.Genres.Add(genre);
        }

        track.Title = command.Title;
        track.Description = command.Description;
        track.IsExplicit = command.IsExplicit;
        track.PictureLink = command.PictureLink; //TODO переписать на вызов сервиса файлов

        await _context.SaveEntitiesAsync();

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userMicroservice.GetUserInfoAsync(track.UserId)
            //TODO добавить оценку
        };
    }
}