using PostRateClient;

namespace TrashGrounds.Post.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddGrpcClient<PostRateService.PostRateServiceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:Rate"] ?? throw new InvalidOperationException());
        });
        
        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<gRPC.Services.PostRateService>();

        return services;
    }
}