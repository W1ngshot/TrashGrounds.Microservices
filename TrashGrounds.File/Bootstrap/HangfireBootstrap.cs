using Hangfire;
using Hangfire.PostgreSql;
using TrashGrounds.File.Hangfire;

namespace TrashGrounds.File.Bootstrap;

public static class HangfireBootstrap
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(configuration.GetConnectionString("Hangfire")));

        services.AddHangfireServer(opt =>
        {
            opt.Queues = new[] {"GC"};
            opt.WorkerCount = 1;
        });

        services.AddSingleton<GarbageCollectorService>();
        
        return services;
    }

    public static void AddHangfireJobs()
    {
        RecurringJob.AddOrUpdate<GarbageCollectorService>("clear-music", "GC", service =>
            service.DeleteUnusedTracks(), Cron.Daily(0, 0));
        RecurringJob.AddOrUpdate<GarbageCollectorService>("clear-images", "GC", service =>
            service.DeleteUnusedImages(), Cron.Daily(0, 0));
    }
}