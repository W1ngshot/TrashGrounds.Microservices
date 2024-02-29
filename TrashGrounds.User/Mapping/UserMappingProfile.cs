using AutoMapper;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Models.Main.User, UserProfile>();
    }
}