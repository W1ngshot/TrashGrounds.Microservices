using MediatR;

namespace TrashGrounds.Rate.Features.Post.DeletePostRate;

public record DeletePostRateCommand(Guid UserId, Guid PostId) : IRequest<bool>;