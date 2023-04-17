namespace TrashGrounds.User.Features.User.ChangePassword;

public record ChangePasswordRequest(Guid UserId, string OldPassword, string NewPassword);