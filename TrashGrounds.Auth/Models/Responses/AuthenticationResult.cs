using TrashGrounds.Auth.Models.Main;

namespace TrashGrounds.Auth.Models.Responses;

public record AuthenticationResult(string Token, RefreshToken RefreshToken);