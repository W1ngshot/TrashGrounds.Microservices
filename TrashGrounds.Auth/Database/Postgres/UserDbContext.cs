using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Auth.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Auth.Models.Main;

namespace TrashGrounds.Auth.Database.Postgres;

public class UserDbContext(
    DbContextOptions<UserDbContext> options,
    IEnumerable<DependencyInjectedEntityConfiguration> configurations)
    : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<AppUser> IdentityUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var configuration in configurations)
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
