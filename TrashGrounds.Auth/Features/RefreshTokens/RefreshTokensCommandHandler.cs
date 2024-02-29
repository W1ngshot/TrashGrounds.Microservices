using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TrashGrounds.Auth.Database.Postgres;
using TrashGrounds.Auth.Infrastructure.Exceptions;
using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Main;
using TrashGrounds.Auth.Models.Responses;
using TrashGrounds.Auth.Services;
using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Features.RefreshTokens;

public class RefreshTokensCommandHandler(
    UserManager<AppUser> userManager,
    UserDbContext context,
    IJwtTokenGenerator jwtTokenGenerator,
    AuthenticationService authService)
    : ICommandHandler<RefreshTokensCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var validationParamsIgnoringTime = jwtTokenGenerator.CloneParameters();
        validationParamsIgnoringTime.ValidateLifetime = false;

        var userId =
            Guid.Parse(
                jwtTokenGenerator.ReadToken(request.Token, validationParamsIgnoringTime)
                    .FindFirst(x => x.Type == ClaimTypes.NameIdentifier)
                    ?.Value
                ?? throw new UnauthorizedException("INVALID_OLD_TOKEN"));

        var applicationUser = await userManager.FindByIdAsync(userId.ToString())
                              ?? throw new NotFoundException<AppUser>();

        var token = applicationUser.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken)
                    ?? throw new UnauthorizedException("INVALID_REFRESH_TOKEN");

        applicationUser.RemoveRefreshToken(token);

        var result = await authService.AuthenticateUser(applicationUser);

        await context.SaveEntitiesAsync();

        return new RefreshTokenResponse(result.Token, result.RefreshToken.Token);
    }
}