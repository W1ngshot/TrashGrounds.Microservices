using MediatR;

namespace TrashGrounds.Auth.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}