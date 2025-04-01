using AutoMapper;
using TrackRateClient;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Mapping;

public class GrpcMappingProfile : Profile
{
    public GrpcMappingProfile()
    {
        CreateMap<TrackRate, Rate>()
            .ConstructUsing(src => new Rate(Guid.Parse(src.Id), src.Rating));
    }
}