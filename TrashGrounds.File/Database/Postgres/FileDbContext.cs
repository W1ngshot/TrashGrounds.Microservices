using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.File.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.File.Models.Main;
using TrashGrounds.File.Options;

namespace TrashGrounds.File.Database.Postgres;

public class FileDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    private readonly IOptions<DatabaseOptions> _dbOptions;

    public required DbSet<MusicFile> MusicFiles { get; set; }
    public required DbSet<ImageFile> ImageFiles { get; set; }

    public FileDbContext(DbContextOptions<FileDbContext> options,
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