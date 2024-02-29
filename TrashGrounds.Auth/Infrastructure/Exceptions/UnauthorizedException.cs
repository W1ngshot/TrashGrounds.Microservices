using System.Net;

namespace TrashGrounds.Auth.Infrastructure.Exceptions;

public class UnauthorizedException(string message) : DomainException(message, (int) HttpStatusCode.Unauthorized);