using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.Comment.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Comment.Options;

namespace TrashGrounds.Comment.Database.Postgres;

public class CommentDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    private readonly IOptions<DatabaseOptions> _dbOptions;

    public required DbSet<Models.Main.Comment> Comments { get; set; }

    public CommentDbContext(DbContextOptions<CommentDbContext> options,
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