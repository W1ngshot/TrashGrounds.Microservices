using TrashGrounds.User.Services;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Bootstrap;

public static class HelperServicesBootstrap
{
    public static IServiceCollection AddHelperServices(this IServiceCollection servicesCollection) =>
        servicesCollection
            .AddAutoMapper(typeof(Program).Assembly)
            .AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<IUserService, UserService>();
}