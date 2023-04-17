namespace TrashGrounds.User.Features.User.ChangeStatus;

public record ChangeStatusRequest(Guid UserId, string NewStatus);