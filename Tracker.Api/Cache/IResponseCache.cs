namespace Tracker.Api.Cache;

public interface IResponseCache {

    Task<string?> GetCacheAsync(string key);

    Task SetCacheAsync(string key, object? response, TimeSpan timeToLive);

}