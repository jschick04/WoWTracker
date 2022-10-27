using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Tracker.Client.Library.Handlers;
using Tracker.Client.Library.Managers.Authentication;
using Tracker.Client.Library.Managers.Interceptors;
using Tracker.Client.Library.StateProviders;
using Tracker.Client.Library.Store;
using Tracker.Library.Managers;

namespace Tracker.Client.Helpers;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddApiHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient(
            "TrackerApi",
            client => client.BaseAddress = new Uri(builder.Configuration["Api"]!)
        ).AddHttpMessageHandler<AuthenticationHeaderHandler>();

        builder.Services.AddScoped(
            sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TrackerApi")
                .EnableIntercept(sp)
        );

        builder.Services.AddHttpClientInterceptor();
    }

    public static void AddBlazorComponents(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredModal();
        builder.Services.AddBlazoredToast();
        builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(RootState).Assembly));
    }

    public static void AddHandlers(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddTransient<AuthenticationHeaderHandler>();
    }

    public static void AddManagers(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddTransient<IHttpInterceptorManager, HttpInterceptorManager>();
        builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        builder.Services.AddScoped<IUserManager, UserManager>();

        builder.Services.AddScoped<ICharacterManager, CharacterManager>();
        builder.Services.AddScoped<IItemManager, ItemManager>();
    }

    public static void AddStateProviders(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<ClientAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();

        builder.Services.AddScoped<ICharacterStateProvider, CharacterStateProvider>();
        builder.Services.AddScoped<INeededItemStateProvider, NeededItemStateProvider>();

        builder.Services.AddAuthorizationCore();
    }
}
