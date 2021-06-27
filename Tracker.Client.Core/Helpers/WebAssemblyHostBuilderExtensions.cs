using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Tracker.Client.Core.Handlers;
using Tracker.Client.Core.Managers.Authentication;
using Tracker.Client.Core.Managers.Interceptors;
using Tracker.Library.Managers;

namespace Tracker.Client.Core.Helpers {

    public static class WebAssemblyHostBuilderExtensions {

        public static void AddApiHttpClient(this WebAssemblyHostBuilder builder) {
            builder.Services.AddHttpClient(
                "TrackerApi",
                client => client.BaseAddress = new Uri(builder.Configuration["Api"])
            ).AddHttpMessageHandler<AuthenticationHeaderHandler>();

            builder.Services.AddScoped(
                sp => sp.GetRequiredService<IHttpClientFactory>()
                    .CreateClient("TrackerApi")
                    .EnableIntercept(sp)
            );

            builder.Services.AddHttpClientInterceptor();
        }

        public static void AddBlazorComponents(this WebAssemblyHostBuilder builder) {
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddMudServices(
                config => {
                    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

                    config.SnackbarConfiguration.ShowCloseIcon = false;
                    config.SnackbarConfiguration.HideTransitionDuration = 100;
                    config.SnackbarConfiguration.ShowTransitionDuration = 100;
                    config.SnackbarConfiguration.VisibleStateDuration = 3000;
                }
            );
        }

        public static void AddHandlers(this WebAssemblyHostBuilder builder) {
            builder.Services.AddTransient<AuthenticationHeaderHandler>();
        }

        public static void AddManagers(this WebAssemblyHostBuilder builder) {
            builder.Services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            builder.Services.AddTransient<IHttpInterceptorManager, HttpInterceptorManager>();
            builder.Services.AddTransient<IUserManager, UserManager>();
        }

        public static void AddServices(this WebAssemblyHostBuilder builder) {
            builder.Services.AddScoped<ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore();
        }

    }

}