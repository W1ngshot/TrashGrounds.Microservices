using TrashGrounds.Track.Models.Additional;
using UserMicroserviceClient;

namespace TrashGrounds.Track.gRPC.Services;

public class UserMicroserviceService
{
    private readonly UserMicroservice.UserMicroserviceClient _userClient;

    public UserMicroserviceService(UserMicroservice.UserMicroserviceClient userClient)
    {
        _userClient = userClient;
    }

    public async Task<UserInformation?> GetUserInfoAsync(Guid id)
    {
        var info = await _userClient.GetUserInfoAsync(new UserInfoRequest {Id = id.ToString()});

        return info?.User is null
            ? null
            : new UserInformation
            {
                Id = Guid.Parse(info.User.Id),
                Nickname = info.User.Nickname,
                AvatarLink = info.User.AvatarLink,
                RegistrationDate = info.User.RegistrationDate.ToDateTime()
            };
    }

    public async Task<IEnumerable<UserInformation>?> GetUsersInfoAsync(IEnumerable<Guid> ids)
    {
        var request = new UsersInfoRequest();
        request.Ids.Add(ids.Select(guid => guid.ToString()));

        var infos = await _userClient.GetUsersInfoAsync(request);

        return infos?.Users?.Select(user => new UserInformation
        {
            Id = Guid.Parse(user.Id),
            Nickname = user.Nickname,
            AvatarLink = user.AvatarLink,
            RegistrationDate = user.RegistrationDate.ToDateTime()
        });
    }
}