using MediatR;

namespace TrashGrounds.Rate.Features.PostGrpc.GetPostRate;

public record GetPostRateQuery(Guid PostId) : IRequest<int>;