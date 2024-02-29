using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Responses;

namespace TrashGrounds.Auth.Features.Register;

public record RegisterCommand(string Email, string Nickname, string Password) : ICommand<AuthorizationResponse>;