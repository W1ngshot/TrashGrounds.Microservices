﻿namespace TrashGrounds.Rate.Infrastructure.Routing;

public interface IEndpointHandler<in TRequest, TResponse>
{
    public Task<TResponse> Handle(TRequest request);
}