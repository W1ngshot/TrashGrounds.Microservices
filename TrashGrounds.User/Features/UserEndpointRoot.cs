using TrashGrounds.User.Features.ChangeAvatar;
using TrashGrounds.User.Features.ChangeStatus;
using TrashGrounds.User.Features.Me;
using TrashGrounds.User.Features.Profile;
using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features;

public class UserEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/user")
            .WithTags("Пользователь")
            .AddEndpoint<GetProfileEndpoint>()
            .AddEndpoint<ChangeAvatarEndpoint>()
            .AddEndpoint<ChangeStatusEndpoint>()
            .AddEndpoint<MeEndpoint>();
    }
}