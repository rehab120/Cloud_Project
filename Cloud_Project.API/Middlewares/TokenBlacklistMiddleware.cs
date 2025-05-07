using Cloud_Project.API.Controllers;
using Cloud_Project.Application.Command.Logout;
using Cloud_Project.Application.Command.LoginMerchant;
using System.Collections.Concurrent;

namespace Cloud_Project.API.Middlewares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = LogOutHandler._blacklistedTokens;

        public TokenBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null && _blacklistedTokens.ContainsKey(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has been invalidated. Please log in again.");
                return;
            }

            await _next(context);
        }
    }
}
