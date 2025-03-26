using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.Post.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Post.Options;

namespace TrashGrounds.Post.Database.Postgres;

public class PostDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    private readonly IOptions<DatabaseOptions> _dbOptions;

    public required DbSet<Models.Main.Post> Posts { get; set; }

    public PostDbContext(DbContextOptions<PostDbContext> options,
        IEnumerable<DependencyInjectedEntityConfiguration> configurations,
        IOptions<DatabaseOptions> dbOptions) : base(options)
    {
        _configurations = configurations;
        _dbOptions = dbOptions;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(_dbOptions.Value.Schema);
        
        foreach (var configuration in _configurations)
            configuration.Configure(builder);
    }

    public async Task<bool> SaveEntitiesAsync()
    {
        await base.SaveChangesAsync();
        return true;
    }
}