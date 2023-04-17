using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.User.Profile;

public class ProfileEndpointHandler : IEndpointHandler<Guid, DomainUser>
{
    private readonly IUserDbContext _context;

    public ProfileEndpointHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<DomainUser> Handle(Guid request)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(x => x.Id == request)
            ?? throw new NotFoundException<DomainUser>();
        //подумать над маппингом и стоит ли отсылать IdentityUserId

        return user;
    }
}