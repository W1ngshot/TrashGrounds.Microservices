
using StackExchange.Redis;

namespace TrashGrounds.Auth.Database.Redis;


public class StackExchangeLock(IDatabase database, string key, string token, bool acquired)
    : IDisposable, IAsyncDisposable
{
    private readonly bool _acquired = acquired;

    public void Dispose()
    {
        database.LockRelease(key, token);
    }

    public async ValueTask DisposeAsync()
    {
        await database.LockReleaseAsync(key, token);
    }
}