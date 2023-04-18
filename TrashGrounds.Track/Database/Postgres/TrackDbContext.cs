using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Database.Postgres;

public class TrackDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public DbSet<Models.Main.Track> Tracks { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public TrackDbContext(DbContextOptions<TrackDbContext> options, 
        IEnumerable<DependencyInjectedEntityConfiguration> configurations) : base(options)
    {
        _configurations = configurations;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var configuration in _configurations)
            configuration.Configure(builder);
    }
}