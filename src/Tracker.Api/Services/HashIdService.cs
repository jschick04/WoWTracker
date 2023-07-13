using Tracker.Api.Managers;
using Tracker.Api.Settings;

namespace Tracker.Api.Services;

public class HashIdService : IServiceInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        var hashIdSettings = new HashIdSettings();

        configuration.Bind(nameof(HashIdSettings), hashIdSettings);
        services.AddSingleton(hashIdSettings);

        services.AddSingleton<IHashIdManager, HashIdManager>();
    }
}
