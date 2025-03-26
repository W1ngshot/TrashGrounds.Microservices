using Hangfire;
using Hangfire.PostgreSql;
using TrashGrounds.File.Hangfire;
using TrashGrounds.File.Options;

namespace TrashGrounds.File.Bootstrap;

public static class HangfireBootstrap
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbOptions = configuration.GetRequiredSection(DatabaseOptions.SectionName).Get<DatabaseOptions>();

        if (string.IsNullOrWhiteSpace(dbOptions?.HangfireSchema))
            throw new ArgumentNullException(nameof(configuration));

        var options = new PostgreSqlStorageOptions
        {
            SchemaName = dbOptions.HangfireSchema
        };

        services.AddHangfire(config =>
            config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(CreateHangfireConnectionString(dbOptions), options));

        services.AddHangfireServer(opt =>
        {
            opt.Queues = new[] { "gc", "default" };
            opt.WorkerCount = 1;
        });

        services.AddScoped<GarbageCollectorService>();

        return services;
    }

    public static void AddHangfireJobs()
    {
        RecurringJob.AddOrUpdate<GarbageCollectorService>("clear-music", service =>
            service.DeleteUnusedTracks(), Cron.Daily);
        RecurringJob.AddOrUpdate<GarbageCollectorService>("clear-images", service =>
            service.DeleteUnusedImages(), Cron.Daily);
    }

    private static string CreateHangfireConnectionString(DatabaseOptions options)
    {
        return $"Host={options.Host};" +
               $"Port={options.Port};" +
               $"Database={options.Database};" +
               $"Username={options.Username};" +
               $"Password={options.Password}";
    }
}