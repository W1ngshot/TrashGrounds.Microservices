using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Database.Postgres;

public class UserDbContext : DbContext, IUserDbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public DbSet<Models.Main.User> DomainUsers { get; set; } = null!;

    public UserDbContext(DbContextOptions<UserDbContext> options, 
        IEnumerable<DependencyInjectedEntityConfiguration> configurations) : base(options)
    {
        _configurations = configurations;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var configuration in _configurations)
            configuration.Configure(builder);

        builder.Entity<IdentityRole<Guid>>().HasData(new List<IdentityRole<Guid>>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "USER"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new()
            {
                Id = Guid.NewGuid(),
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
