﻿using Microsoft.EntityFrameworkCore;
using Npgsql;
using TrashGrounds.Auth.Database.Postgres;

namespace TrashGrounds.Auth.Infrastructure;

public static class MigrateDatabase
{
    public static async Task TryMigrateDatabaseAsync(this WebApplication app)
    {
        try
        {
            await using var scope = app.Services.CreateAsyncScope();
            var sp = scope.ServiceProvider;
            var db = sp.GetRequiredService<UserDbContext>();
    
            await db.Database.MigrateAsync();

            await using var conn = (NpgsqlConnection)db.Database.GetDbConnection();
            await conn.OpenAsync();
            await conn.ReloadTypesAsync();
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Error while migrating the database");
            Environment.Exit(-1);
        }
    }
}