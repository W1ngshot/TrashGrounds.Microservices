using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Auth.Database.Postgres;
using TrashGrounds.Auth.Models.Main;
using TrashGrounds.Auth.Services.Configs;

namespace TrashGrounds.Auth.Bootstrap;

public static class UsersDatabaseBootstrap
{
    public static IServiceCollection AddDatabaseWithIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, IdentityRole<Guid>>(options=> {
                options.Password.RequiredLength = PasswordConfig.MinimumLength;
                options.Password.RequireNonAlphanumeric = PasswordConfig.RequireNonAlphanumeric;
                options.Password.RequireLowercase = PasswordConfig.RequireLowercase;
                options.Password.RequireUppercase = PasswordConfig.RequireUppercase;
                options.Password.RequireDigit = PasswordConfig.RequireDigit;
            })
            .AddEntityFrameworkStores<UserDbContext>();
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}