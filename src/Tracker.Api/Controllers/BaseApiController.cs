using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Tracker.Api.Entities;

namespace Tracker.Api.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    // Returns the current authenticated user (null if not logged in)
    public User? Account => (User?)HttpContext.Items["Account"];

    protected string GetIpAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out StringValues header))
        {
            return header!;
        }

        // TODO: Add check for IPv6
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown IP";
    }

    protected void SetRefreshTokenHeader(string refreshToken)
    {
        if (Response.Headers.TryGetValue("RefreshToken", out var _))
        {
            Response.Headers.Remove("RefreshToken");
        }

        Response.Headers.Add("RefreshToken", refreshToken);
    }
}
