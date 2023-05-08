using AutoMapper;
using TrashGrounds.Track.Models.Additional;
using UserMicroserviceClient;

namespace TrashGrounds.Track.gRPC.Services;

public class UserInfoService
{
    private readonly UserMicroservice.UserMicroserviceClient _userClient;
    private readonly IMapper _mapper;

    public UserInfoService(UserMicroservice.UserMicroserviceClient userClient, IMapper mapper)
    {
        _userClient = userClient;
        _mapper = mapper;
    }

    public async Task<UserInformation?> GetUserInfoAsync(Guid id)
    {
        try
        {
            var info = await _userClient.GetUserInfoAsync(new UserInfoRequest {Id = id.ToString()});

            return info?.User is null ? null : _mapper.Map<UserInfo, UserInformation>(info.User);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<UserInformation>?> GetUsersInfoAsync(IEnumerable<Guid> ids)
    {
        try
        {
            var request = new UsersInfoRequest();
            request.Ids.Add(ids.Select(guid => guid.ToString()));

            var infos = await _userClient.GetUsersInfoAsync(request);

            return infos?.Users?.Select(user => _mapper.Map<UserInfo, UserInformation>(user));
        }
        catch (Exception e)
        {
            return null;
        }
    }
}