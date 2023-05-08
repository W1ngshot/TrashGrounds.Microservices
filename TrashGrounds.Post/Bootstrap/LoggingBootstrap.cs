﻿using Serilog;

namespace TrashGrounds.Post.Bootstrap;

public static class LoggingBootstrap 
{
    public static void AddCustomLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, _, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
            configuration.Enrich.FromLogContext();
            configuration.Enrich.WithProperty("Application", "TrashGrounds.Post");
            configuration.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
            configuration.WriteTo.Console();
        });
    }
}