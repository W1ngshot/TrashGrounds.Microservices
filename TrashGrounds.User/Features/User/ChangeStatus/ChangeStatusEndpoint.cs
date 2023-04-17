using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.User.ChangeStatus;

public class ChangeStatusEndpoint : IEndpoint
{
    public record ChangeStatusDto(string NewStatus);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/change-status",
                async (ChangeStatusDto dto, IUserService userService, ChangeStatusEndpointHandler handler) =>
                    Results.Ok(await handler.Handle(
                        new ChangeStatusRequest(userService.GetUserIdOrThrow(), dto.NewStatus))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<ChangeStatusDto>());
    }
}