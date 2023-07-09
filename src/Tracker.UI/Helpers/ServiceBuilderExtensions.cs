using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Client.Library.Features;
using Tracker.Client.Library.Handlers;
using Tracker.Client.Library.Managers.Authentication;
using Tracker.Client.Library.Managers.Interceptors;
using Tracker.Client.Library.StateProviders;
using Tracker.Library.Managers;

namespace Tracker.UI.Helpers;

public static class ServiceBuilderExtensions
{
    public static void AddBlazorComponents(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
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
        services.AddScoped<IUserManager, UserManager>();

        services.AddScoped<ICharacterManager, CharacterManager>();
        services.AddScoped<IItemManager, ItemManager>();
    }

    public static void AddStateProviders(this IServiceCollection services)
    {
        services.AddScoped<ClientAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();

        services.AddScoped<ICharacterStateProvider, CharacterStateProvider>();
        services.AddScoped<INeededItemStateProvider, NeededItemStateProvider>();

        services.AddAuthorizationCore();
    }
}
