using TrashGrounds.Track.Bootstrap;
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
    .AddAuthorizationWithPolicy();

builder.Services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

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