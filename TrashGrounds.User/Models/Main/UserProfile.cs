namespace TrashGrounds.User.Models.Main;

public record UserProfile(
    Guid Id,
    string Nickname,
    DateTime RegistrationDate,
    string? AvatarLink,
    string? Status);