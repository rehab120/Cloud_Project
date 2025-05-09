using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Cloud_Project.Application.Common.Interfaces;
using MediatR;

namespace Cloud_Project.Application.Command.Logout
{
    public record LogOutCommand(string token):IRequest<LogOutResult>;
    public record LogOutResult(bool success,List<string> Errors);

    //public class LogOutHandler :IRequestHandler<LogOutCommand, LogOutResult>
    //{
    //    public static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();

    //    public LogOutHandler()
    //    {

    //    }
    //    public async Task<LogOutResult> Handle(LogOutCommand command,CancellationToken cancellationToken)
    //    {
    //        var token = command.token;
    //        if (token == null)
    //        {
    //            return new(false, new() { "No token provided" });
    //        }

    //        var handler = new JwtSecurityTokenHandler();
    //        var jwtToken = handler.ReadJwtToken(token);
    //        var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

    //        if (expClaim == null)
    //        {
    //            return new(true, new() { "Invalid token" });
    //        }

    //        var expiryDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime;

    //        // Store token in blacklist until it naturally expires
    //        _blacklistedTokens[token] = expiryDate;

    //        return new(true, new() { "Logged out successfully" });

    //    }

    //}

    public class LogOutHandler : IRequestHandler<LogOutCommand, LogOutResult>
    {
        private readonly IIdentityService identityService;

        public LogOutHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<LogOutResult> Handle(LogOutCommand command, CancellationToken cancellationToken)
        {
            return await identityService.LogoutAsync(command.token);
        }
    }

}
