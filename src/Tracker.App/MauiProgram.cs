using System.Reflection;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Tracker.App.Services;
using Tracker.Client.Library.Handlers;
using Tracker.UI.Helpers;

namespace Tracker.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        // Temporary until proper appsettings support is added
        // https://github.com/dotnet/maui/issues/4408
        var app = Assembly.GetExecutingAssembly();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        using var stream = app.GetManifestResourceStream("Tracker.App.appsettings.Development.json");
#else
        using var stream = app.GetManifestResourceStream("Tracker.App.appsettings.json");
#endif

        // Not wrapping this in a try because app needs to crash if there is no appsettings
        var appsettings = JsonSerializer.Deserialize<Dictionary<string, string>>(stream!);

        builder.Services.AddHttpClient(
            "TrackerApi",
            // TODO: Temp until appsettings works with MAUI
            client => client.BaseAddress = new Uri(appsettings["Api"])
        ).AddHttpMessageHandler<AuthenticationHeaderHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("TrackerApi")
            .EnableIntercept(sp));

        builder.Services.AddHttpClientInterceptor();

        builder.Services.AddHandlers();

        builder.Services.AddStateProviders();
        builder.Services.AddManagers();

        builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
        builder.Services.AddBlazorComponents();

        return builder.Build();
    }
}
