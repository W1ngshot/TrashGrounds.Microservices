namespace TrashGrounds.User.Services.SupportTypes;

public static class AuthConfig
{
    public const int TokenLifetime = 10 * 60;

    public const int RefreshTokenLifetime = 7 * 24 * 60 * 60;
}
