using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres;
using TrashGrounds.User.Database.Postgres.Interfaces;

namespace TrashGrounds.User.Bootstrap;

public static class UsersDatabaseBootstrap
{
    public static IServiceCollection AddDatabaseWithIdentity(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IUserDbContext, UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}