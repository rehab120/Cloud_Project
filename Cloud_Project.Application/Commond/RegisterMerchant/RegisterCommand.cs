using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Cloud_Project.Application.Commond.RegisterMerchant
{
    public record RegisterCommand(string UserName, string Email, string Password) : IRequest<RegisterResult>;
    public record RegisterResult(bool Success,List<string> Errors);


    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly UserManager<IdentityUser> userManager;
        public RegisterHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<RegisterResult> Handle(RegisterCommand command,CancellationToken cancellationToken)
        {
            var User = new IdentityUser
            {
                UserName = command.UserName,
                Email = command.Email,
            };
            var result = await userManager.CreateAsync(User,command.Password);
            await userManager.AddToRoleAsync(User, "Merchant");
            if (result.Succeeded)
            {
                return new RegisterResult(true, new());
            }
            return new RegisterResult(false, result.Errors.Select(e => e.Description).ToList());
            

        }
    }
}
