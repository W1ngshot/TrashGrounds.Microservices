using MediatR;
using TrashGrounds.Auth.Infrastructure.Routing;
using TrashGrounds.Auth.Infrastructure.ValidationSetup;
using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Features.ChangePassword;

public class ChangePasswordEndpoint : IEndpoint
{
    public record ChangePasswordDto(string OldPassword, string NewPassword);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("/change-password",
                async (ChangePasswordDto dto, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new ChangePasswordCommand(
                        userService.GetUserIdOrThrow(),
                        dto.OldPassword,
                        dto.NewPassword))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<ChangePasswordDto>());
    }
}