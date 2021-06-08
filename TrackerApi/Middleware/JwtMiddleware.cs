using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TrackerApi.Data;
using TrackerApi.Managers;

namespace TrackerApi.Middleware {

    public class JwtMiddleware {

        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, DataContext data, ITokenManager tokenManager) {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenManager.ValidateToken(token);

            if (userId is not null) { context.Items["Account"] = await data.Users.FindAsync(userId); }

            await _next(context);
        }

    }

}