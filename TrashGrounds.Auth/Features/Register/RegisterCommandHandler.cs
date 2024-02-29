using Microsoft.AspNetCore.Identity;
using TrashGrounds.Auth.Database.Postgres;
using TrashGrounds.Auth.Infrastructure.Exceptions;
using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Main;
using TrashGrounds.Auth.Models.Responses;
using TrashGrounds.Auth.Services;

namespace TrashGrounds.Auth.Features.Register;

public class RegisterCommandHandler(
    UserManager<AppUser> userManager,
    AuthenticationService authService,
    UserDbContext dbContext)
    : ICommandHandler<RegisterCommand, AuthorizationResponse>
{
    public async Task<AuthorizationResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //TODO отправить запрос на создание DomainUser в User микросервис
        var identityUser = new AppUser
        {
            Email = command.Email,
            UserName = command.Nickname
        };
        var result = await userManager.CreateAsync(identityUser, command.Password);
        var roleResult = await userManager.AddToRoleAsync(identityUser, "User");

        if (!result.Succeeded)
            throw new UnauthorizedException(string.Join("\n", result.Errors.Select(x => x.Description)));
        if (!roleResult.Succeeded)
            throw new UnauthorizedException(string.Join("\n", roleResult.Errors.Select(x => x.Description)));

        var tokens = await authService.AuthenticateUser(identityUser);
        await dbContext.SaveEntitiesAsync();
        return AuthorizationResponse.FromAuthenticationResult(tokens);
    }
}