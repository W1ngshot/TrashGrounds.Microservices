using TrashGrounds.Auth.Bootstrap;
using TrashGrounds.Auth.Infrastructure;
using TrashGrounds.Auth.Infrastructure.Routing;
using TrashGrounds.Auth.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services
    .AddDatabaseWithIdentity(builder.Configuration);
//.AddRedis(builder.Configuration);

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

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
app.UseHttpsRedirection();
app.UseCors();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomEndpoints();

app.Run();