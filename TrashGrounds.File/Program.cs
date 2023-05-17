using Hangfire;
using Microsoft.AspNetCore.Http.Features;
using TrashGrounds.File.Bootstrap;
using TrashGrounds.File.gRPC.Services;
using TrashGrounds.File.Infrastructure;
using TrashGrounds.File.Infrastructure.Routing;
using TrashGrounds.File.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 20971520; // 20MB limit
});

builder.Services
    .AddDatabase(builder.Configuration)
    .AddGrpc();

builder.Services
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAuthorizationWithPolicy();

builder.Services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>())
    .AddHangfireConfiguration(builder.Configuration);

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

app.UseHangfireDashboard();
HangfireBootstrap.AddHangfireJobs();

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
app.MapHangfireDashboard();
app.MapGrpcService<FileExistsService>();

app.Run();