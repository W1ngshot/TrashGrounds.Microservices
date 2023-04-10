using Microsoft.AspNetCore.Identity;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Exceptions;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;
using TrashGrounds.User.Routing;
using TrashGrounds.User.Services;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.Auth.Register;

public class RegisterEndpointHandler : IEndpointHandler<RegisterRequest, AuthorizationResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserDbContext _dbContext;
    private readonly AuthenticationService _authService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterEndpointHandler(UserManager<AppUser> userManager,
        AuthenticationService authService,
        IUserDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
    {
        _userManager = userManager;
        _authService = authService;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<AuthorizationResponse> Handle(RegisterRequest request)
    {
        var identityUser = new AppUser
        {
            Email = request.Email,
            UserName = request.Nickname
        };
        var result = await _userManager.CreateAsync(identityUser, request.Password);
        var roleResult = await _userManager.AddToRoleAsync(identityUser, "User");
        
        if (!result.Succeeded)
            throw new UnauthorizedException(string.Join("\n", result.Errors.Select(x => x.Description)));
        if (!roleResult.Succeeded)
            throw new UnauthorizedException(string.Join("\n", roleResult.Errors.Select(x => x.Description)));

        var domainUser = new DomainUser
        {
            Nickname = request.Nickname,
            IdentityUserId = identityUser.Id,
            RegistrationDate = _dateTimeProvider.UtcNow
        };
        await _dbContext.DomainUsers.AddAsync(domainUser);

        var tokens = await _authService.AuthenticateUser(identityUser, domainUser);
        await _dbContext.SaveEntitiesAsync();
        return AuthorizationResponse.FromAuthenticationResult(tokens);
    }
}
