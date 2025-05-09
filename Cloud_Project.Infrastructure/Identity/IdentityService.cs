using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Application.Command.Logout;
using Cloud_Project.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cloud_Project.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private static readonly ConcurrentDictionary<string, DateTime> blacklistedTokens = new();


        public IdentityService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<(bool Success, List<string> Errors)> RegisterUserAsync(string username, string email, string password, string phoneNumber, string role)
        {
            var user = new IdentityUser { UserName = username, Email = email, PhoneNumber = phoneNumber };
            return await Register(user, password, role);
        }

        public async Task<(bool Success, List<string> Errors)> RegisterUserAsync(string username, string email, string password, string role)
        {
            var user = new ApplicationUser { UserName = username, Email = email };
            return await Register(user, password, role);
        }

        public async Task<(bool Success, List<string> Errors)> Register(IdentityUser user, string password, string role)
        {
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToList());

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            var roleResult = await userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
                return (false, roleResult.Errors.Select(e => e.Description).ToList());

            return (true, new List<string>());
        }

        public async Task<(bool Success, string Token, List<string> Errors)> LoginAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                return (false, null, new() { "Invalid username or password" });

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return (false, null, new() { "Invalid username or password" });

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secretKey = configuration["JWT:SecretKey"];
            var issuer = configuration["JWT:Issuer"];
            var audience = configuration["JWT:Audience"];

            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentNullException("JWT:SecretKey");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (true, tokenString, new());
        }

        public async Task<IdentityUser?> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<LogOutResult> LogoutAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return new LogOutResult(false, new() { "No token provided" });

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var exp = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (exp == null)
                return new LogOutResult(false, new() { "Invalid token" });

            var expiry = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;
            blacklistedTokens[token] = expiry;

            return new LogOutResult(true, new() { "Logged out successfully" });
        }

        public bool IsTokenBlacklisted(string token)
        {
            return blacklistedTokens.ContainsKey(token);
        }







    }
}
