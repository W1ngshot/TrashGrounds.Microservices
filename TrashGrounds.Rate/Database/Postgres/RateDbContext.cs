using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.Rate.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Rate.Models.Main;
using TrashGrounds.Rate.Options;

namespace TrashGrounds.Rate.Database.Postgres;

public class RateDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    private readonly IOptions<DatabaseOptions> _dbOptions;

    public required DbSet<TrackRate> TrackRates { get; set; }

    public required DbSet<PostRate> PostRates { get; set; }

    public RateDbContext(DbContextOptions<RateDbContext> options,
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