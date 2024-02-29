using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Database.Postgres.Interfaces;

public interface IUserDbContext
{
    public DbSet<Models.Main.User> DomainUsers { get; set; }

    public Task<bool> SaveEntitiesAsync();
}