using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Tracker.Api.Cache;

public class RedisResponseCache : IResponseCache
{
    private readonly IDistributedCache _cache;

    public RedisResponseCache(IDistributedCache cache) => _cache = cache;

    public async Task<string?> GetCacheAsync(string key)
    {
        var cachedResponse = await _cache.GetStringAsync(key);

        return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
    }

    public async Task SetCacheAsync(string key, object? response, TimeSpan timeToLive)
    {
        if (response is null) { return; }

        var serializedResponse = JsonSerializer.Serialize(response);

        await _cache.SetStringAsync(
            key,
            serializedResponse,
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = timeToLive }
        );
    }
}
