using TrashGrounds.Template.Infrastructure.Mediator.Query;
using TrashGrounds.Template.Models.Additional;

namespace TrashGrounds.Template.Features.Music.GetMusic;

public record GetMusicQuery(Guid FileId) : IQuery<FileResponse>;