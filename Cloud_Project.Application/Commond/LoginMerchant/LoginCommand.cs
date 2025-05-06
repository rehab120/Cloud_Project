using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Concurrent;

namespace Cloud_Project.Application.Commond.LoginMerchant
{
    public record LoginCommand(string UserName, string Password):IRequest<LoginResult>;
    public record LoginResult(bool success, string Token, List<string> Errors);

    public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        public static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();

        public LoginHandler(UserManager<IdentityUser> userManager, IConfiguration _configuration, SignInManager<IdentityUser> _signInManager)
        {
            this.userManager = userManager;
            this._configuration = _configuration;
            this._signInManager = _signInManager;
        }
        public async Task<LoginResult> Handle(LoginCommand command,CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(command.UserName);
            if (user == null)
            {
                return new(false, null, new(){"Invalid email or password" });
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (!result.Succeeded)
            {
                return new(false, null, new() { "Invalid email or password" });
            }
            //token
            var claims = new List<Claim>();
            //claims.Add(new Claim("name","value"));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            }
            //signingCredentials
            var secretKey = _configuration["JWT:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("JWT:SecretKey", "JWT SecretKey is missing in configuration.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: sc

                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new(true, tokenString, new());
        }







    }

}
