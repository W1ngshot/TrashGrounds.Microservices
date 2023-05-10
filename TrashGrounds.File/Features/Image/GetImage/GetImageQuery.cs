using TrashGrounds.Template.Infrastructure.Mediator.Query;
using TrashGrounds.Template.Models.Additional;

namespace TrashGrounds.Template.Features.Image.GetImage;

public record GetImageQuery(Guid FileId) : IQuery<FileResponse>;