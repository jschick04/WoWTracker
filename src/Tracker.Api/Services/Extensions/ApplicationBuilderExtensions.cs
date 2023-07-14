using Hangfire;
using HangfireBasicAuthenticationFilter;
using Tracker.Api.Middleware;
using Tracker.Api.Settings;

namespace Tracker.Api.Services.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureHangfire(this IApplicationBuilder app, IConfiguration config)
    {
        var hangfireSettings = new HangfireSettings();
        config.Bind(nameof(HangfireSettings), hangfireSettings);

        //app.UseHangfireDashboard(
        //    "/scheduler",
        //    new DashboardOptions {
        //        AppPath = null,
        //        Authorization = new[] { new HangfireAuthorizationFilter() },
        //        DashboardTitle = "WoW Tracker Scheduler"
        //    }
        //);

        app.UseHangfireDashboard(
            "/scheduler",
            new DashboardOptions
            {
                AppPath = null,
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = hangfireSettings.Username,
                        Pass = hangfireSettings.Password
                    }
                },
                DashboardTitle = "WoW Tracker Scheduler"
            }
        );
    }

    public static void ConfigureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<JwtMiddleware>();
    }

    public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration config)
    {
        var settings = new SwaggerSettings();
        config.Bind(nameof(SwaggerSettings), settings);

        app.UseSwagger(options => { options.RouteTemplate = settings.JsonRoute; });

        app.UseSwaggerUI(
            options =>
            {
                options.SwaggerEndpoint(settings.UiEndpoint, settings.Description);
                options.DisplayRequestDuration();
            }
        );
    }
}
