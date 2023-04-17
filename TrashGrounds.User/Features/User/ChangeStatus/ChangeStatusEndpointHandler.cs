using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.User.ChangeStatus;

public class ChangeStatusEndpointHandler : IEndpointHandler<ChangeStatusRequest, SuccessResponse>
{
    private readonly IUserDbContext _context;

    public ChangeStatusEndpointHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<SuccessResponse> Handle(ChangeStatusRequest request)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(user => user.Id == request.UserId)
            ?? throw new NotFoundException<DomainUser>();

        user.Status = request.NewStatus;
        
        await _context.SaveEntitiesAsync();

        return new SuccessResponse();
    }
}