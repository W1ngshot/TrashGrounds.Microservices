using System.Net;

namespace TrashGrounds.Auth.Infrastructure.Exceptions;

public class ForbiddenException(string message) : DomainException(message, (int) HttpStatusCode.Forbidden);
