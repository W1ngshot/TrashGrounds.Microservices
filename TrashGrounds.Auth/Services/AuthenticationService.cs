using Microsoft.AspNetCore.Identity;
using TrashGrounds.Auth.Infrastructure.Constants;
using TrashGrounds.Auth.Infrastructure.Exceptions;
using TrashGrounds.Auth.Models.Main;
using TrashGrounds.Auth.Models.Responses;
using TrashGrounds.Auth.Services.Configs;
using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Services;

public class AuthenticationService(
    IJwtTokenGenerator jwtTokenGenerator,
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager)
{
    public async Task<AuthenticationResult> AuthenticateUser(AppUser identityUser)
    {
        var refreshToken = jwtTokenGenerator.GenerateRefreshToken();
        var roles = await userManager.GetRolesAsync(identityUser);
        var token = jwtTokenGenerator.GenerateUserToken(identityUser, roles,
            DateTime.UtcNow.AddSeconds(TokensConfig.TokenLifetime));

        identityUser.AddRefreshToken(refreshToken);

        return new AuthenticationResult(token, refreshToken);
    }

    public async Task<AuthenticationResult> ProcessPasswordLogin(string email, string password)
    {
        var identityUser = await userManager.FindByEmailAsync(email)
                           ?? throw new NotFoundException<AppUser>();

        var result = await signInManager.CheckPasswordSignInAsync(identityUser, password, false);

        if (!result.Succeeded)
            throw new ValidationFailedException("Password", ValidationFailedMessages.WrongPassword);

        return await AuthenticateUser(identityUser);
    }
}