using TrashGrounds.Track.gRPC.Services;
using UserMicroserviceClient;

namespace TrashGrounds.Track.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services)
    {
        services.AddGrpc();

        services.AddGrpcClient<UserMicroservice.UserMicroserviceClient>(options =>
        {
            options.Address = new Uri("https://localhost:7035");
        });
        
        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<UserMicroserviceService>();

        return services;
    }
}