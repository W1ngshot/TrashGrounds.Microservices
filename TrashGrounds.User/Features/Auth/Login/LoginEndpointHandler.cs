using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginEndpointHandler : IEndpointHandler<LoginRequest, AuthorizationResponse>
{
    private readonly IUserDbContext _context;
    private readonly Services.AuthenticationService _authService;
    public LoginEndpointHandler(IUserDbContext context, Services.AuthenticationService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<AuthorizationResponse> Handle(LoginRequest request)
    {
        //TODO сделать авторизацию по почте или логину, а не только логину
        var result = AuthorizationResponse.FromAuthenticationResult(
            await _authService.ProcessPasswordLogin(
                request.Email
                ?? throw new ValidationFailedException(nameof(request.Email), "EMAIL_IS_NULL"),
                request.Password
                ?? throw new ValidationFailedException(nameof(request.Password), "PASSWORD_IS_NULL")));

        await _context.SaveEntitiesAsync();
        return result;
    }
}