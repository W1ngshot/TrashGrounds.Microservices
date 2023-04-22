﻿using Serilog;

namespace TrashGrounds.Track.Bootstrap;

public static class LoggingBootstrap 
{
    public static void AddCustomLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, _, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
            configuration.Enrich.FromLogContext();
            configuration.Enrich.WithProperty("Application", "TrashGrounds.Track");
            configuration.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
            configuration.WriteTo.Console();
        });
    }
}