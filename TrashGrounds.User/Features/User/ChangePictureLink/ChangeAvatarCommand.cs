﻿using TrashGrounds.User.Infrastructure.Mediator.Command;

namespace TrashGrounds.User.Features.User.ChangePictureLink;

public record ChangeAvatarCommand(Guid UserId, string NewLink) : ICommand<ChangeAvatarResponse>;