using MediatR;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Post.Database.Postgres;

namespace TrashGrounds.Post.Features.Post.GetUserPosts;

public class GetUserPostsQueryHandler : IRequestHandler<GetUserPostsQuery, IEnumerable<Models.Main.Post>>
{
    private readonly PostDbContext _context;

    public GetUserPostsQueryHandler(PostDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Main.Post>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
            .Where(post => post.UserId == request.UserId && (request.ShowHidden || !post.IsHidden))
            .OrderByDescending(post => post.UploadDate)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        //получение оценки
        //возможно получение пользователя
        
        return posts;
    }
};