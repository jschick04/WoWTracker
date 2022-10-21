using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tracker.Client;
using Tracker.Client.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.AddHandlers();
builder.AddApiHttpClient();

builder.AddServices();
builder.AddManagers();

builder.AddBlazorComponents();

await builder.Build().RunAsync();
