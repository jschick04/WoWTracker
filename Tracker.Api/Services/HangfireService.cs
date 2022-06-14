using Hangfire;
using System.Data.SqlClient;

namespace Tracker.Api.Services;

public class HangfireService : IServiceInstaller {

    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
        var builder = new SqlConnectionStringBuilder(configuration.GetConnectionString("TrackerDb")) {
            ApplicationName = "Hangfire"
        };

        services.AddHangfire(
            config => config.UseSqlServerStorage(builder.ConnectionString)
                .WithJobExpirationTimeout(TimeSpan.FromDays(7))
        );

        services.AddHangfireServer();
    }

}