using Tracker.Api.Cache;
using Tracker.Api.Settings;

namespace Tracker.Api.Services;

public class CacheService : IServiceInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        var cacheSettings = new CacheSettings();

        configuration.Bind(nameof(CacheSettings), cacheSettings);
        services.AddSingleton(cacheSettings);

        if (!cacheSettings.Enabled) { return; }

        services.AddStackExchangeRedisCache(options => { options.Configuration = cacheSettings.ConnectionString; });
        services.AddSingleton<IResponseCache, RedisResponseCache>();
    }
}
