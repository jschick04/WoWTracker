using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Tracker.Api.Middleware;
using Tracker.Api.Settings;

namespace Tracker.Api.Services.Extensions {

    public static class ApplicationBuilderExtensions {

        public static void ConfigureMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
        }

        public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration config) {
            var settings = new SwaggerSettings();
            config.Bind(nameof(SwaggerSettings), settings);

            app.UseSwagger(options => { options.RouteTemplate = settings.JsonRoute; });

            app.UseSwaggerUI(
                options => {
                    options.SwaggerEndpoint(settings.UiEndpoint, settings.Description);
                    options.DisplayRequestDuration();
                }
            );
        }

    }

}