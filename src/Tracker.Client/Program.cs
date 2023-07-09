using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Tracker.Client;
using Tracker.Client.Library.Handlers;
using Tracker.UI.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient(
    "TrackerApi",
    client => client.BaseAddress = new Uri(builder.Configuration["Api"]!)
).AddHttpMessageHandler<AuthenticationHeaderHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("TrackerApi")
    .EnableIntercept(sp));

builder.Services.AddHttpClientInterceptor();

builder.Services.AddHandlers();

builder.Services.AddStateProviders();
builder.Services.AddManagers();

builder.Services.AddBlazorComponents();

await builder.Build().RunAsync();
