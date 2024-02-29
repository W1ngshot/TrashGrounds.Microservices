using System.Security.Claims;
using TrashGrounds.Auth.Infrastructure.Exceptions;
using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Services;


public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private Guid? UserId => Guid.TryParse(
        httpContextAccessor.HttpContext
            ?.User
            ?.FindFirstValue(ClaimTypes.NameIdentifier),
        out var userId)
        ? userId
        : null;

    public Guid GetUserIdOrThrow() => UserId ?? throw new UnauthorizedException("Unauthorized");
}