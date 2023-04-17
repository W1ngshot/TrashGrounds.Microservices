﻿using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.User.ChangePassword;

public class ChangePasswordEndpoint : IEndpoint
{
    public record ChangePasswordDto(string OldPassword, string NewPassword);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/change-password",
                async (ChangePasswordDto dto, IUserService userService, ChangePasswordEndpointHandler handler) =>
                    Results.Ok(await handler.Handle(
                        new ChangePasswordRequest(
                            userService.GetUserIdOrThrow(),
                            dto.OldPassword,
                            dto.NewPassword))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<ChangePasswordDto>());
    }
}