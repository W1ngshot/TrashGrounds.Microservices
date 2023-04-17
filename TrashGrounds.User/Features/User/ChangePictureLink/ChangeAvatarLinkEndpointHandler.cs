using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.User.ChangePictureLink;

public class ChangeAvatarLinkEndpointHandler : IEndpointHandler<ChangeAvatarLinkRequest, SuccessResponse>
{
    private readonly IUserDbContext _context;

    public ChangeAvatarLinkEndpointHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<SuccessResponse> Handle(ChangeAvatarLinkRequest request)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(user => user.Id == request.UserId)
                   ?? throw new NotFoundException<DomainUser>();

        user.AvatarLink = request.NewLink;
        
        await _context.SaveEntitiesAsync();

        return new SuccessResponse();
    }
}