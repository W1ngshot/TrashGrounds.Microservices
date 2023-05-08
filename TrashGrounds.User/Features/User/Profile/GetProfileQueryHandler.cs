using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Query;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.User.Profile;

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, DomainUser>
{
    private readonly IUserDbContext _context;

    public GetProfileQueryHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<DomainUser> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(user => user.Id == request.UserId,
                       cancellationToken: cancellationToken)
                   ?? throw new NotFoundException<DomainUser>();
        //подумать над маппингом и стоит ли отсылать IdentityUserId

        return user;
    }
}