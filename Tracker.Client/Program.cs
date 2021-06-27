using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tracker.Client.Core.Helpers;

namespace Tracker.Client {

    public class Program {

        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.AddHandlers();
            builder.AddApiHttpClient();

            builder.AddServices();
            builder.AddManagers();

            builder.AddBlazorComponents();

            await builder.Build().RunAsync();
        }

    }

}