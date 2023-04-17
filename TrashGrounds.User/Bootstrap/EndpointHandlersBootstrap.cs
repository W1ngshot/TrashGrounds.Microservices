using TrashGrounds.User.Features.Auth.Login;
using TrashGrounds.User.Features.Auth.RefreshToken;
using TrashGrounds.User.Features.Auth.Register;
using TrashGrounds.User.Features.User.ChangePassword;
using TrashGrounds.User.Features.User.ChangePictureLink;
using TrashGrounds.User.Features.User.ChangeStatus;
using TrashGrounds.User.Features.User.Profile;
using TrashGrounds.User.Features.User.UsersInfo;

namespace TrashGrounds.User.Bootstrap;

public static class EndpointHandlersBootstrap
{
    public static IServiceCollection AddCustomEndpointHandlers(this IServiceCollection services)
    {
        services //auth handlers
            .AddScoped<RefreshTokenEndpointHandler>()
            .AddScoped<LoginEndpointHandler>()
            .AddScoped<RegisterEndpointHandler>();

        services //user handlers
            .AddScoped<ProfileEndpointHandler>()
            .AddScoped<ChangeStatusEndpointHandler>()
            .AddScoped<ChangeAvatarLinkEndpointHandler>()
            .AddScoped<ChangePasswordEndpointHandler>()
            .AddScoped<UsersInfoEndpointHandler>();
        
        return services;
    }
    //TODO переделать
}