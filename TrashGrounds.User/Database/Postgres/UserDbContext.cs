using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrashGrounds.User.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Options;

namespace TrashGrounds.User.Database.Postgres;

public class UserDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUserDbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    private readonly IOptions<DatabaseOptions> _dbOptions;

    public DbSet<AppUser> IdentityUsers { get; set; } = null!;
    public DbSet<DomainUser> DomainUsers { get; set; } = null!;

    public UserDbContext(DbContextOptions<UserDbContext> options,
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

        builder.Entity<IdentityRole<Guid>>().HasData(new List<IdentityRole<Guid>>
        {
            new()
            {
                Id = Guid.Parse("e73aafb0-5637-4e5c-8497-ec20791dcd93"),
                Name = "User",
                NormalizedName = "USER"
            },
            new()
            {
                Id = Guid.Parse("a1942a56-edb8-400c-a8b2-b6e114564b06"),
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new()
            {
                Id = Guid.Parse("941f7059-873f-40e0-b7b7-871bf0297a88"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        });
    }

    public async Task<bool> SaveEntitiesAsync()
    {
        await base.SaveChangesAsync();
        return true;
    }
}