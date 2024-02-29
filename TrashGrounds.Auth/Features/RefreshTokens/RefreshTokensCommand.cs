using TrashGrounds.Auth.Infrastructure.Mediator.Command;
using TrashGrounds.Auth.Models.Responses;

namespace TrashGrounds.Auth.Features.RefreshTokens;

public record RefreshTokensCommand(string Token, string RefreshToken) : ICommand<RefreshTokenResponse>;