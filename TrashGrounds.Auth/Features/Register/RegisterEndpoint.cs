using MediatR;
using TrashGrounds.Auth.Infrastructure.Routing;
using TrashGrounds.Auth.Infrastructure.ValidationSetup;

namespace TrashGrounds.Auth.Features.Register;

public class RegisterEndpoint : IEndpoint
{
    public record RegisterDto(string Email, string Nickname, string Password);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/register", async (RegisterDto dto, IMediator mediator) =>
                Results.Ok(await mediator.Send(new RegisterCommand(
                    dto.Email,
                    dto.Nickname,
                    dto.Password))))
            .AddValidation(builder => builder.AddFor<RegisterDto>());
    }
}