using TrashGrounds.Template.Infrastructure.Mediator.Command;

namespace TrashGrounds.Template.Features.Music.UploadMusic;

public record UploadMusicCommand(IFormFile File, Guid UserId) : ICommand<Guid>;