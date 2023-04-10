namespace TrashGrounds.User.Services.SupportTypes;

public static class AuthConfig
{
    public static readonly int TokenLifetime = 10 * 60;
    
    public static readonly int RefreshTokenLifetime = 7 * 24 * 60 * 60;
}
