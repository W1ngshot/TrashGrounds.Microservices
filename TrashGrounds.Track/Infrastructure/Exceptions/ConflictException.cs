using System.Net;

namespace TrashGrounds.Track.Infrastructure.Exceptions;

public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message, (int)HttpStatusCode.Conflict)
    {
    }
}