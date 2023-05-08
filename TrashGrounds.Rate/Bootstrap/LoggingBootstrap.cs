using Serilog;

namespace TrashGrounds.Rate.Bootstrap;

public static class LoggingBootstrap 
{
    public static void AddCustomLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, _, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
            configuration.Enrich.FromLogContext();
            configuration.Enrich.WithProperty("Application", "TrashGrounds.Rate");
            configuration.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
            configuration.WriteTo.Console();
        });
    }
}