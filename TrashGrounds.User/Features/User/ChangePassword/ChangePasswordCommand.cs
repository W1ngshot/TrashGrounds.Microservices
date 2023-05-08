using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.User.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : ICommand<SuccessResponse>;