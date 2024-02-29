using TrashGrounds.Auth.Database.Postgres.Configurations;
using TrashGrounds.Auth.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Auth.Database.Postgres;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<DependencyInjectedEntityConfiguration, AppUserConfiguration>();
        
        return services;
    }
}