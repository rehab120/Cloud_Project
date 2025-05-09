using Cloud_Project.API.Controllers;
using Cloud_Project.Application.Command.Logout;
using Cloud_Project.Application.Command.LoginMerchant;
using System.Collections.Concurrent;
using Cloud_Project.Application.Common.Interfaces;

namespace Cloud_Project.API.Middlewares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public TokenBlacklistMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                using var scope = _scopeFactory.CreateScope();
                var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();

                if (identityService.IsTokenBlacklisted(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token has been invalidated. Please log in again.");
                    return;
                }
            }

            await _next(context);
        }
    }

}
