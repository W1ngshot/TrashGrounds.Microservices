using TrashGrounds.User.Database.Postgres.Configurations;
using TrashGrounds.User.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.User.Bootstrap;

public static class ConfigureEntitiesConfiguration 
{
    public static IServiceCollection AddCustomEntitiesConfiguration(this IServiceCollection services) =>
        services
            .AddSingleton<DependencyInjectedEntityConfiguration, ApplicationUserConfiguration>()
            .AddSingleton<DependencyInjectedEntityConfiguration, DomainUserConfiguration>();
}