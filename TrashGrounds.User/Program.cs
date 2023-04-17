using TrashGrounds.User.Bootstrap;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services
    .AddDatabaseWithIdentity(builder.Configuration)
    .AddRedis(builder.Configuration); ;

builder.Services
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAuthorizationWithPolicy()
    .AddCustomEndpointHandlers();

builder.Services
    .AddHelperServices()
    .AddFluentValidation();

var app = builder.Build();

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

app.Run();