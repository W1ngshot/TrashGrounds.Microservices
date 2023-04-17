using TrashGrounds.User.Features.User.ChangePassword;
using TrashGrounds.User.Features.User.ChangePictureLink;
using TrashGrounds.User.Features.User.ChangeStatus;
using TrashGrounds.User.Features.User.Me;
using TrashGrounds.User.Features.User.Profile;
using TrashGrounds.User.Features.User.UsersInfo;
using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features.User;

public class UserEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/user")
            .WithTags("Пользователь")
            .AddEndpoint<ProfileEndpoint>()
            .AddEndpoint<ChangeAvatarLinkEndpoint>()
            .AddEndpoint<ChangeStatusEndpoint>()
            .AddEndpoint<ChangePasswordEndpoint>()
            .AddEndpoint<MeEndpoint>()
            .AddEndpoint<UsersInfoEndpoint>();
    }
}