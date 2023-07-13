namespace Tracker.Api.Services.Extensions;

public static class ServiceInstallerExtensions
{
    public static void InstallServicesInAssembly(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env
    )
    {
        var installers = typeof(Startup).Assembly.ExportedTypes.Where(
            x => typeof(IServiceInstaller).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false }
        ).Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

        installers.ForEach(installer => installer.InstallService(services, configuration, env));
    }
}
