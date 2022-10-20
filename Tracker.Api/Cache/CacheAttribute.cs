using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tracker.Api.Entities;
using Tracker.Api.Settings;

namespace Tracker.Api.Cache;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CacheAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeToLiveSeconds;

    public CacheAttribute(int timeToLiveSeconds) => _timeToLiveSeconds = timeToLiveSeconds;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<CacheSettings>();

        if (!cacheSettings.Enabled) { return; }

        var cache = context.HttpContext.RequestServices.GetRequiredService<IResponseCache>();

        // TODO: Find a better way to do this without needing Api.Entities
        var user = (User?)context.HttpContext.Items["Account"];

        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request, user?.Username);
        var cachedResponse = await cache.GetCacheAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedResponse))
        {
            var contentResult = new ContentResult
            {
                Content = cachedResponse,
                ContentType = "application/json",
                StatusCode = 200
            };

            context.Result = contentResult;

            return;
        }

        var executedContext = await next();

        if (executedContext.Result is OkObjectResult ok)
        {
            await cache.SetCacheAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
        }
    }

    private static string GenerateCacheKeyFromRequest(HttpRequest request, string? username)
    {
        var keyBuilder = new StringBuilder();

        if (username is not null)
        {
            keyBuilder.Append($"{username}|");
        }

        keyBuilder.Append($"{request.Path}");

        foreach ((string key, var value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"|{key}-{value}");
        }

        return keyBuilder.ToString();
    }
}
