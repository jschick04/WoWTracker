using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Entities;

namespace TrackerApi.Controllers {

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

        protected void SetTokenCookie(string refreshToken) {
            var cookieOptions = new CookieOptions {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }

    }

}