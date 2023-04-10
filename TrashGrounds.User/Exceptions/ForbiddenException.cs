using System.Net;

namespace TrashGrounds.User.Exceptions;

public class ForbiddenException: DomainException
{
    public ForbiddenException(string message) : base(message, (int)HttpStatusCode.Forbidden)
    {
    }
}
