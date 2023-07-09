using Hangfire.Annotations;
using Hangfire.Dashboard;
using Tracker.Api.Managers;

namespace Tracker.Api.Filters;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        // TODO: Fix so we can use JWT instead of Basic Auth
        var httpContext = context.GetHttpContext();

        //var user = httpContext.User; // This should be populated by the JWT middleware, don't need below code

        string? token;

        if (httpContext.Request.Query.ContainsKey("token"))
        {
            token = httpContext.Request.Query["token"];

            if (string.IsNullOrEmpty(token)) { return false; }

            httpContext.Response.Headers.Add("Authorization", $"Bearer {token}");

            httpContext.Response.Cookies.Append(
                "Jobs",
                token,
                new CookieOptions { Expires = DateTime.Now.AddMinutes(15) }
            );
        }
        else
        {
            token = httpContext.Request.Cookies["Jobs"];
        }

        if (string.IsNullOrEmpty(token)) { return false; }

        var tokenManager = httpContext.RequestServices.GetService(typeof(ITokenManager)) as TokenManager;

        return tokenManager?.IsAdminClaim(token) is true;
    }
}
