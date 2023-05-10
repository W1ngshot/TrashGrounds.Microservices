using TrashGrounds.Template.Infrastructure.Mediator.Command;

namespace TrashGrounds.Template.Features.Image.UploadImage;

public record UploadImageCommand(IFormFile File, Guid UserId) : ICommand<Guid>;