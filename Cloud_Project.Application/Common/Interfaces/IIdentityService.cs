using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Application.Command.Logout;
using Microsoft.AspNetCore.Identity;

namespace Cloud_Project.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Success, List<string> Errors)> RegisterUserAsync(string username, string email, string password, string phoneNumber, string role);
        Task<(bool Success, List<string> Errors)> RegisterUserAsync(string username, string email, string password, string role);
        Task<(bool Success, List<string> Errors)> Register(IdentityUser user, string password, string role);
        Task<(bool Success, string Token, List<string> Errors)> LoginAsync(string username, string password);
        Task<IdentityUser?> GetUserByEmailAsync(string email);
        Task<LogOutResult> LogoutAsync(string token);
        bool IsTokenBlacklisted(string token);

    }
}
