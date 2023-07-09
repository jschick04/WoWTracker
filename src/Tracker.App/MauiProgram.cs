using Microsoft.Extensions.Logging;
using Toolbelt.Blazor.Extensions.DependencyInjection;
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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient(
            "TrackerApi",
            // TODO: Temp until appsettings works with MAUI
            client => client.BaseAddress = new Uri("https://wowtrackerapi.azurewebsites.net")
        ).AddHttpMessageHandler<AuthenticationHeaderHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("TrackerApi")
            .EnableIntercept(sp));

        builder.Services.AddHttpClientInterceptor();

        builder.Services.AddHandlers();

        builder.Services.AddStateProviders();
        builder.Services.AddManagers();

        builder.Services.AddBlazorComponents();

        return builder.Build();
    }
}
