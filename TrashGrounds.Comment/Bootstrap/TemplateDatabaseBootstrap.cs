using Microsoft.EntityFrameworkCore;
using TrashGrounds.Comment.Database.Postgres;

namespace TrashGrounds.Comment.Bootstrap;

public static class TemplateDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CommentDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}