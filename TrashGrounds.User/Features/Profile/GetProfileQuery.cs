using TrashGrounds.User.Infrastructure.Mediator.Query;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.Profile;

public record GetProfileQuery(Guid UserId) : IQuery<UserProfile>;