﻿using TrashGrounds.Rate.Features.Post.ChangePostRate;
using TrashGrounds.Rate.Features.Post.DeletePostRate;
using TrashGrounds.Rate.Features.Post.GetPostsUserRates;
using TrashGrounds.Rate.Features.Post.GetPostUserRate;
using TrashGrounds.Rate.Infrastructure.Routing;

namespace TrashGrounds.Rate.Features.Post;

public class PostRateEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/post-rate")
            .WithTags("Оценка поста")
            .AddEndpoint<GetPostUserRateEndpoint>()
            .AddEndpoint<ChangePostRateEndpoint>()
            .AddEndpoint<DeletePostRateEndpoint>()
            .AddEndpoint<GetPostsUserRatesEndpoint>();
    }
}