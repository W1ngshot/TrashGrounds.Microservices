using AutoMapper;
using TrackRateClient;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;
using UserMicroserviceClient;

namespace TrashGrounds.Track.Mapping;

public class GrpcMappingProfile : Profile
{
    public GrpcMappingProfile()
    {
        CreateMap<UserInfo, UserInformation>()
            .ForMember(dest => dest.RegistrationDate,
                opt =>
                    opt.MapFrom(src => src.RegistrationDate.ToDateTime()))
            .ForMember(dest => dest.Id,
                opt => 
                    opt.MapFrom(src => Guid.Parse(src.Id)));

        CreateMap<TrackRate, Rate>()
            .ForMember(dest => dest.TrackId,
                opt =>
                    opt.MapFrom(src => Guid.Parse(src.Id)));
    }
}