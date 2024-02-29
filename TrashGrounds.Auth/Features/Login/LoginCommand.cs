using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Responses;

namespace TrashGrounds.Auth.Features.Login;

public record LoginCommand(string Email, string Password) : ICommand<AuthorizationResponse>;