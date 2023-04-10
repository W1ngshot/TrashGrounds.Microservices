using TrashGrounds.User.Features.Auth.Login;
using TrashGrounds.User.Features.Auth.RefreshToken;
using TrashGrounds.User.Features.Auth.Register;

namespace TrashGrounds.User.Bootstrap;

public static class EndpointHandlersBootstrap
{
    public static IServiceCollection
        AddCustomEndpointHandlersConfiguration(this IServiceCollection servicesCollection) =>
        servicesCollection
            .AddScoped<RefreshTokenEndpointHandler>()
            .AddScoped<LoginEndpointHandler>()
            .AddScoped<RegisterEndpointHandler>();
    //TODO переделать
}