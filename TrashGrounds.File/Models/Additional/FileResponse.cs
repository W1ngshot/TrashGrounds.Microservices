namespace TrashGrounds.Template.Models.Additional;

public record FileResponse(MemoryStream Stream, string ContentType, Guid FileId);