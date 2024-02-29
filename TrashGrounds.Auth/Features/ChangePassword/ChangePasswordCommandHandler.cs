using Microsoft.AspNetCore.Identity;
using TrashGrounds.Auth.Infrastructure.Exceptions;
using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Main;
using TrashGrounds.Auth.Models.Responses;

namespace TrashGrounds.Auth.Features.ChangePassword;

public class ChangePasswordCommandHandler(UserManager<AppUser> userManager)
    : ICommandHandler<ChangePasswordCommand, SuccessResponse>
{
    public async Task<SuccessResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var appUser = await userManager.FindByIdAsync(request.UserId.ToString())
                      ?? throw new NotFoundException<AppUser>();

        var result = await userManager.ChangePasswordAsync(appUser, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
            throw new ValidationFailedException("Password", result.Errors.First().Description);

        return new SuccessResponse();
    }
}