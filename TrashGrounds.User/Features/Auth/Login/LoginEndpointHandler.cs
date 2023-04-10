using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Exceptions;
using TrashGrounds.User.Models.Responses;
using TrashGrounds.User.Routing;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginEndpointHandler : IEndpointHandler<AuthorizationRequest, AuthorizationResponse>
{
    private readonly IUserDbContext _dbContext;
    private readonly Services.AuthenticationService _authService;
    public LoginEndpointHandler(IUserDbContext dbContext, Services.AuthenticationService authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }

    public async Task<AuthorizationResponse> Handle(AuthorizationRequest request)
    {
        //TODO сделать авторизацию по почте или логину
        var result = AuthorizationResponse.FromAuthenticationResult(
            await _authService.ProcessPasswordLogin(
                request.Email
                ?? throw new ValidationFailedException(nameof(request.Email), "EMAIL_IS_NULL"),
                request.Password
                ?? throw new ValidationFailedException(nameof(request.Password), "PASSWORD_IS_NULL")));

        await _dbContext.SaveEntitiesAsync();
        return result;
    }
}