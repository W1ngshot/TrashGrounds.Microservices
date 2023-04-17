using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features.User.UsersInfo;

public class UsersInfoEndpoint : IEndpoint
{
    public record UsersInfoDto(IEnumerable<Guid> Ids)
    {
        public static ValueTask<UsersInfoDto> BindAsync(HttpContext context)
        {
            var queries = context.Request.Query[nameof(Ids)];
            var result = new UsersInfoDto(queries.Select(Guid.Parse!).ToList());
            return ValueTask.FromResult(result);
        }
    }
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/users-info",
            async (UsersInfoDto dto, UsersInfoEndpointHandler handler) =>
                Results.Ok(await handler.Handle(new UsersInfoRequest(dto.Ids))));
    }
}