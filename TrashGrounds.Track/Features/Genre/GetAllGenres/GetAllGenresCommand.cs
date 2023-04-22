using MediatR;

namespace TrashGrounds.Track.Features.Genre.GetAllGenres;

public record GetAllGenresCommand() : IRequest<IEnumerable<Models.Main.Genre>>;