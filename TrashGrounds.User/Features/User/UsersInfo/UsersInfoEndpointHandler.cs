using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.User.UsersInfo;

public class UsersInfoEndpointHandler : IEndpointHandler<UsersInfoRequest, IEnumerable<UserInfo>>
{
    private readonly IUserDbContext _context;

    public UsersInfoEndpointHandler(IUserDbContext context)
    {
        _context = context;
    }

    //Получение части инфы пользователя для комментов или треков по листу id
    public async Task<IEnumerable<UserInfo>> Handle(UsersInfoRequest request)
    {
        return await _context.DomainUsers
            .Where(user => request.UserIds.Contains(user.Id))
            .Select(user => new UserInfo 
                {
                    Id = user.Id,
                    Nickname = user.Nickname, 
                    RegistrationDate = user.RegistrationDate, 
                    AvatarLink = user.AvatarLink
                    
                }).ToListAsync();
    }
}