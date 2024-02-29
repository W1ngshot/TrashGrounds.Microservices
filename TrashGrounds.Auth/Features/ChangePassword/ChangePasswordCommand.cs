using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Responses;

namespace TrashGrounds.Auth.Features.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : ICommand<SuccessResponse>;