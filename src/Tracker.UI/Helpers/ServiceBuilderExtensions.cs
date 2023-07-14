using Blazored.Modal;
using Blazored.Toast;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Tracker.UI.Library.Features;
using Tracker.UI.Library.Handlers;
using Tracker.UI.Library.Managers.Authentication;
using Tracker.UI.Library.Managers.Interceptors;
using Tracker.UI.Library.StateProviders;

namespace Tracker.UI.Helpers;

public static class ServiceBuilderExtensions
{
    public static void AddBlazorComponents(this IServiceCollection services)
    {
        services.AddBlazoredModal();
        services.AddBlazoredToast();
        services.AddFluxor(options => options.ScanAssemblies(typeof(RootState).Assembly));
    }

    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<AuthenticationHeaderHandler>();
    }

    public static void AddManagers(this IServiceCollection services)
    {
        services.AddTransient<IHttpInterceptorManager, HttpInterceptorManager>();
        services.AddScoped<IAuthenticationManager, AuthenticationManager>();
    }

    public static void AddStateProviders(this IServiceCollection services)
    {
        services.AddScoped<ClientAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();

        services.AddScoped<ICharacterStateProvider, CharacterStateProvider>();
        services.AddScoped<INeededItemStateProvider, NeededItemStateProvider>();
        services.AddScoped<IProfessionStateProvider, ProfessionStateProvider>();

        services.AddAuthorizationCore();
    }
}
