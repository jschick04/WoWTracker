using Microsoft.Extensions.DependencyInjection;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.Core;

public static class IoC
{
    private static ServiceProvider _serviceProvider;

    public static T Get<T>() => _serviceProvider.GetService<T>();

    public static void Initialize()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ApplicationViewModel>();
        services.AddSingleton<ShellViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<SummaryViewModel>();
    }
}
