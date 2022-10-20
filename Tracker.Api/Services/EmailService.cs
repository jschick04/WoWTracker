using Tracker.Api.Managers;
using Tracker.Api.Settings;

namespace Tracker.Api.Services;

public class EmailService : IServiceInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        var emailSettings = new EmailSettings();
        configuration.Bind(nameof(EmailSettings), emailSettings);

        services.AddFluentEmail(emailSettings.From, env.ApplicationName).AddSendGridSender(emailSettings.ApiKey);

        services.AddTransient<IEmailManager, EmailManager>();
    }
}
