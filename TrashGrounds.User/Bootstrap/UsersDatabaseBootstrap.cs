using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Bootstrap;

public static class UsersDatabaseBootstrap
{
    public static IServiceCollection AddDatabaseWithIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UserDbContext>();
        services.AddDbContext<IUserDbContext,UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}