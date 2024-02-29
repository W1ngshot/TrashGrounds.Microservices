using MediatR;

namespace TrashGrounds.Auth.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{

}