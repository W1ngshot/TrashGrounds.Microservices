﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.Post.Database.Postgres;
using TrashGrounds.Post.Options;

namespace TrashGrounds.Post.Bootstrap;

public static class PostDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

        services.AddDbContext<PostDbContext>((serviceProvider, dbOptions) =>
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