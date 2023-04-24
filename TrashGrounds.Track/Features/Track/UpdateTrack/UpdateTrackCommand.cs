﻿using MediatR;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public record UpdateTrackCommand(Guid UserId, Guid TrackId, string Title, string? Description,
    bool IsExplicit, string? PictureLink, IEnumerable<Guid> Genres) : IRequest<FullTrack>;