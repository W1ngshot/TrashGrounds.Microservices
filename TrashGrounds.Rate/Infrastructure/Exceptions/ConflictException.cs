using System.Net;

namespace TrashGrounds.Rate.Infrastructure.Exceptions;

public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message, (int)HttpStatusCode.Conflict)
    {
    }
}