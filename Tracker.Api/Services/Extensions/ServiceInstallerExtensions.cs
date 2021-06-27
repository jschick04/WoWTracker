using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker.Api.Services.Extensions {

    public static class ServiceInstallerExtensions {

        public static void InstallServicesInAssembly(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env
        ) {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(
                x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract
            ).Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

            installers.ForEach(installer => installer.InstallService(services, configuration, env));
        }

    }

}