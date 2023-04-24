﻿using TrashGrounds.Track.gRPC.Services;
using UserMicroserviceClient;

namespace TrashGrounds.Track.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddGrpcClient<UserMicroservice.UserMicroserviceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:User"] ?? throw new InvalidOperationException());
        });
        
        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<UserMicroserviceService>();

        return services;
    }
}