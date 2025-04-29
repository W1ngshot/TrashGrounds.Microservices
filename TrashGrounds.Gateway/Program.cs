using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelot(
    builder.Environment.IsDevelopment() ? "./OcelotDev" : "./Ocelot", 
    builder.Environment);

builder.Services.AddCors();
    

builder.Services
    .AddOcelot()
    .AddCacheManager(builderCachePart => builderCachePart.WithDictionaryHandle());

var app = builder.Build();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowCredentials();
    option.SetIsOriginAllowed(_ => true);
});

app.MapGet("/test", () => "Hello world from Ocelot Gateway");

await app.UseOcelot();

app.Run();