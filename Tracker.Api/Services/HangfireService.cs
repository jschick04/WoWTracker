using Hangfire;

namespace Tracker.Api.Services;

public class HangfireService : IServiceInstaller {

    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
        services.AddHangfire(
            config => config.UseSqlServerStorage(configuration.GetConnectionString("TrackerDb"))
                .WithJobExpirationTimeout(TimeSpan.FromDays(7))
        );

        services.AddHangfireServer();
    }

}