using TrashGrounds.Auth.Services;
using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Bootstrap;

public static class HelperServicesBootstrap
{
    public static IServiceCollection AddHelperServices(this IServiceCollection servicesCollection) =>
        servicesCollection
            .AddAutoMapper(typeof(Program).Assembly)
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<AuthenticationService>()
            .AddScoped<IUserService, UserService>();
}