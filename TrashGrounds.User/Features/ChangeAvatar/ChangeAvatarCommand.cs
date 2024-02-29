using TrashGrounds.User.Infrastructure.Mediator.Command;

namespace TrashGrounds.User.Features.ChangeAvatar;

public record ChangeAvatarCommand(Guid UserId, Guid? NewAvatarId) : ICommand<ChangeAvatarResponse>;