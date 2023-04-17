using Microsoft.AspNetCore.Identity;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.User.ChangePassword;

public class ChangePasswordEndpointHandler : IEndpointHandler<ChangePasswordRequest, SuccessResponse>
{
    private readonly IUserDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ChangePasswordEndpointHandler(IUserDbContext context, 
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<SuccessResponse> Handle(ChangePasswordRequest request)
    {
        var user = await _context.DomainUsers.FirstOrNotFoundAsync(user => user.Id == request.UserId);

        var appUser = await _userManager.FindByIdAsync(user.IdentityUserId.ToString())
                      ?? throw new NotFoundException<AppUser>();

        var result = await _userManager.ChangePasswordAsync(appUser, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
            throw new ValidationFailedException("Password", result.Errors.First().Description);

        return new SuccessResponse();
    }
}