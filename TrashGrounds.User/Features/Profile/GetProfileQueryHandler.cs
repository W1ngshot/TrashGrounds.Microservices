using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Query;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.Profile;

public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, UserProfile>
{
    private readonly IUserDbContext _context;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(IUserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserProfile> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(user => user.Id == request.UserId,
                       cancellationToken: cancellationToken)
                   ?? throw new NotFoundException<Models.Main.User>();

        return _mapper.Map<Models.Main.User, UserProfile>(user);
    }
}