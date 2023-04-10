using System.Net;
using TrashGrounds.User.Constants;

namespace TrashGrounds.User.Exceptions;

public class BadRequestException<T> : DomainException
{
    public BadRequestException() : base(
        ErrorCodes.BadRequestError, (int)HttpStatusCode.BadRequest)
    {
        PlaceholderData.Add("EntityName", typeof(T).Name);
    }
}
public class BadRequestException : DomainException
{
    public BadRequestException(string message) : base(
        message, (int)HttpStatusCode.BadRequest)
    {
    }
}