using MediatR;

namespace TrashGrounds.Template.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
    
}