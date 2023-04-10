using System.Net;
using TrashGrounds.User.Constants;

namespace TrashGrounds.User.Exceptions;

public class NotFoundException<T> : DomainException
{
    public NotFoundException() : base(
        ErrorCodes.NotFoundError, (int)HttpStatusCode.NotFound)
    {
        PlaceholderData.Add("EntityName", typeof(T).Name);
    }
}