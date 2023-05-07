using MediatR;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.PostGrpc.GetPostsRate;

public record GetPostsRateQuery(IEnumerable<Guid> Ids) : IRequest<PostsRateResponse>;