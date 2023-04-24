using TrashGrounds.Track.Services;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Bootstrap;

public static class HelperServicesBootstrap
{
    public static IServiceCollection AddHelperServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IUserService, UserService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}