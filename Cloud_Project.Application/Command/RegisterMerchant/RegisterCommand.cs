using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cloud_Project.Application.Command.RegisterMerchant
{
    public record RegisterCommand(string UserName, string Email, string Password) : IRequest<RegisterResult>;
    public record RegisterResult(bool Success, List<string> Errors);

    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = command.UserName,
                Email = command.Email,
            };

            var result = await userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                return new RegisterResult(false, result.Errors.Select(e => e.Description).ToList());
            }

            if (!await roleManager.RoleExistsAsync("Merchant"))
            {
                await roleManager.CreateAsync(new IdentityRole("Merchant"));
            }

            var roleResult = await userManager.AddToRoleAsync(user, "Merchant");
            if (!roleResult.Succeeded)
            {
                return new RegisterResult(false, roleResult.Errors.Select(e => e.Description).ToList());
            }

            return new RegisterResult(true, new());
        }
    }
}
