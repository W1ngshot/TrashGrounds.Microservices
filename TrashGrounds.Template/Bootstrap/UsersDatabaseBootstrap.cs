using Microsoft.EntityFrameworkCore;
using TrashGrounds.Template.Database.Postgres;

namespace TrashGrounds.Template.Bootstrap;

public static class TracksDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TrackDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}