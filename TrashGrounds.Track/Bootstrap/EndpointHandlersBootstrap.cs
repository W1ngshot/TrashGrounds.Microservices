using TrashGrounds.Track.Features.Track.AddTrack;
using TrashGrounds.Track.Features.Track.GetTrack;
using TrashGrounds.Track.Features.Track.GetTrackByCategory;
using TrashGrounds.Track.Features.Track.GetTracksFromUser;
using TrashGrounds.Track.Features.Track.SearchTracks;
using TrashGrounds.Track.Features.Track.UpdateTrack;

namespace TrashGrounds.Track.Bootstrap;

public static class EndpointHandlersBootstrap
{
    public static IServiceCollection AddCustomEndpointHandlers(this IServiceCollection services)
    {
        services //track handlers
            .AddScoped<AddTrackEndpointHandler>()
            .AddScoped<GetTrackEndpointHandler>()
            .AddScoped<GetTrackByCategoryEndpointHandler>()
            .AddScoped<GetTracksFromUserEndpointHandler>()
            .AddScoped<SearchTracksEndpointHandler>()
            .AddScoped<UpdateTrackEndpointHandler>();
        
        return services;
    }
}