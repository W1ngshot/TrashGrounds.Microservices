using System.Security.Claims;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Services.Extensions;

public static class JwtGeneratorExtensions
{
    public static string GenerateUserToken(this IJwtTokenGenerator generator, DomainUser user,
        IEnumerable<string> roles, DateTime expiration) 
        => generator.GenerateFromClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, roles.First())
        }, expiration);
}