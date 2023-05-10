using MediatR;

namespace TrashGrounds.Template.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}