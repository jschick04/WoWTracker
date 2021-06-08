using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrackerApi.Services {

    public interface IServiceInstaller {

        void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env);

    }

}