using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.User.Database.Postgres;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Options;
using TrashGrounds.User.Services.Configs;

namespace TrashGrounds.User.Bootstrap;

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

        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

        services.AddDbContext<IUserDbContext, UserDbContext>((serviceProvider, dbOptions) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var connectionString = CreateConnectionString(options);

            dbOptions.UseNpgsql(connectionString,
                builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", options.Schema));
        });
        
        services.AddDatabaseConfigurations();

        return services;
    }
    
    private static string CreateConnectionString(DatabaseOptions options)
    {
        return $"Host={options.Host};" +
               $"Port={options.Port};" +
               $"Database={options.Database};" +
               $"Username={options.Username};" +
               $"Password={options.Password};" +
               $"SearchPath={options.Schema}";
    }
}