using TrashGrounds.Track.Bootstrap;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services
    .AddDatabase(builder.Configuration);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCustomEndpointHandlers();

builder.Services
    .AddFluentValidation();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCustomEndpoints();

app.Run();