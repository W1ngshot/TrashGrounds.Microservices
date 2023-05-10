using Microsoft.AspNetCore.Http.Features;
using TrashGrounds.Template.Bootstrap;
using TrashGrounds.Template.gRPC.Services;
using TrashGrounds.Template.Infrastructure;
using TrashGrounds.Template.Infrastructure.Routing;
using TrashGrounds.Template.Middleware;

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
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

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
app.MapGrpcService<FileExistsService>();

app.Run();