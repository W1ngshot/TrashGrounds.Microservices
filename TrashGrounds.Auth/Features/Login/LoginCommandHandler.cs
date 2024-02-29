using TrashGrounds.Auth.Database.Postgres;
using TrashGrounds.Auth.Infrastructure.Exceptions;
using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Responses;

namespace TrashGrounds.Auth.Features.Login;

public class LoginCommandHandler(UserDbContext context, Services.AuthenticationService authService)
    : ICommandHandler<LoginCommand, AuthorizationResponse>
{
    public async Task<AuthorizationResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        //TODO переделать авторизацию с логина на почту
        var result = AuthorizationResponse.FromAuthenticationResult(
            await authService.ProcessPasswordLogin(
                command.Email
                ?? throw new ValidationFailedException(nameof(command.Email), "EMAIL_IS_NULL"),
                command.Password
                ?? throw new ValidationFailedException(nameof(command.Password), "PASSWORD_IS_NULL")));

        await context.SaveEntitiesAsync();
        return result;
    }
}