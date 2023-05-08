﻿using MediatR;

namespace TrashGrounds.Comment.Infrastructure.Mediator.Command;

public interface ICommandHandler<in TCommand, TOut> : IRequestHandler<TCommand, TOut>
    where TCommand : ICommand<TOut>
{
}