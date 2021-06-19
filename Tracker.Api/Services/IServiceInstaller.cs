using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker.Api.Services {

    public interface IServiceInstaller {

        void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env);

    }

}