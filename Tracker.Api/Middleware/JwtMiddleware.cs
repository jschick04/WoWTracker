using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tracker.Api.Data;
using Tracker.Api.Managers;

namespace Tracker.Api.Middleware {

    public class JwtMiddleware {

        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, DataContext data, ITokenManager tokenManager) {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token) && tokenManager.GetUserId(token, out var userId)) {
                context.Items["Account"] = await data.Users.FindAsync(userId);
            }

            await _next(context);
        }

    }

}