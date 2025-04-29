using Hangfire;
using Microsoft.AspNetCore.Http.Features;
using TrashGrounds.File.Bootstrap;
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
    .AddDatabase(builder.Configuration);

builder.Services
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAuthorizationWithPolicy()
    .AddCors();

builder.Services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>())
    .AddHangfireConfiguration(builder.Configuration);

builder.Services
    .AddGrpcConfiguration(builder.Configuration)
    .AddGrpcServices();

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowCredentials();
    option.SetIsOriginAllowed(_ => true);
});

app.UseHangfireDashboard();

//if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomEndpoints();
app.MapHangfireDashboard();
app.MapGrpcServices();

HangfireBootstrap.AddHangfireJobs();
app.Run();