using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Entities;

namespace Tracker.Api.Controllers {

    [ApiController]
    public class BaseApiController : ControllerBase {

        // Returns the current authenticated user (null if not logged in)
        public User Account => (User)HttpContext.Items["Account"];

        protected string GetIpAddress() {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) {
                return Request.Headers["X-Forwarded-For"];
            }

            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

        protected void SetRefreshTokenHeader(string refreshToken) {
            if (Response.Headers.TryGetValue("RefreshToken", out var _)) {
                Response.Headers.Remove("RefreshToken");
            }

            Response.Headers.Add("RefreshToken", refreshToken);
        }

    }

}