using TrashGrounds.Track.Bootstrap;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddCustomLogging();

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
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

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

//if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
//
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomEndpoints();
app.MapGrpcServices();

app.Run();