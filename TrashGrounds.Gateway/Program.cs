using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelot(
    builder.Environment.IsDevelopment() ? "./OcelotDev" : "./Ocelot", 
    builder.Environment);

builder.Services
    .AddOcelot()
    .AddCacheManager(builderCachePart => builderCachePart.WithDictionaryHandle());

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();