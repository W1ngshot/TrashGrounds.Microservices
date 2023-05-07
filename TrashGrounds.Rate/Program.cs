using TrashGrounds.Rate.Bootstrap;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

builder.Services
    .AddDatabase(builder.Configuration)
    .AddGrpc();;

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