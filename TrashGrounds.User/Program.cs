using TrashGrounds.User.Bootstrap;
using TrashGrounds.User.gRPC.Services;
using TrashGrounds.User.Infrastructure;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services
    .AddDatabaseWithIdentity(builder.Configuration)
    .AddRedis(builder.Configuration)
    .AddGrpc();

builder.Services
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAuthorizationWithPolicy();

builder.Services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());;

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomEndpoints();
app.MapGrpcService<UserInfoGrpcService>();

app.Run();