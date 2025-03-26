using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.Track.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Track.Models.Main;
using TrashGrounds.Track.Options;

namespace TrashGrounds.Track.Database.Postgres;

public class TrackDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    private readonly IOptions<DatabaseOptions> _dbOptions;

    public required DbSet<MusicTrack> MusicTracks { get; set; }
    public required DbSet<Genre> Genres { get; set; }

    public TrackDbContext(DbContextOptions<TrackDbContext> options,
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

        builder.Entity<Genre>().HasData(new List<Genre>
        {
            new()
            {
                Name = "Русский рэп",
            },
            new()
            {
                Name = "Что-то странное",
            },
            new()
            {
                Name = "Мэшап",
            }
        });
    }

    public async Task<bool> SaveEntitiesAsync()
    {
        await base.SaveChangesAsync();
        return true;
    }
}