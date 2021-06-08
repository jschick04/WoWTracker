using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackerApi.Managers;
using TrackerApi.Settings;

namespace TrackerApi.Services {

    public class EmailService : IServiceInstaller {

        public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            var emailSettings = new EmailSettings();
            configuration.Bind(nameof(EmailSettings), emailSettings);
            services.AddSingleton(emailSettings);

            services.AddTransient<IEmailManager, EmailManager>();
        }

    }

}