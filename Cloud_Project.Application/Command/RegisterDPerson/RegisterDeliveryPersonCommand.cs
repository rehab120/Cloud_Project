using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Application.Common.Interfaces;


using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cloud_Project.Application.Command.RegisterDPerson
{
    public record RegisterDeliveryPersonCommand(string UserName, string Email, string Password, string PhoneNumber) : IRequest<RegisterDeliveryPersonResult>;
    public record RegisterDeliveryPersonResult(bool Success, List<string> Errors);

    //public class RegisterDeliveryHandler : IRequestHandler<RegisterDeliveryPersonCommand, RegisterDeliveryPersonResult>
    //{
    //    private readonly UserManager<IdentityUser> userManager;
    //    private readonly RoleManager<IdentityRole> roleManager;
    //    private readonly IDeliveryPersonRepositry deliveryPersonRepositry;
    //    public RegisterDeliveryHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IDeliveryPersonRepositry deliveryPersonRepositry)
    //    {
    //        this.userManager = userManager;
    //        this.roleManager = roleManager;
    //        this.deliveryPersonRepositry = deliveryPersonRepositry;
    //    }

    //    public async Task<RegisterDeliveryPersonResult> Handle(RegisterDeliveryPersonCommand command, CancellationToken cancellationToken)
    //    {
    //        var user = new IdentityUser
    //        {
    //            UserName = command.UserName,
    //            Email = command.Email,
    //            PhoneNumber = command.PhoneNumber
    //        };

    //        var result = await userManager.CreateAsync(user, command.Password);
    //        if (!result.Succeeded)
    //        {
    //            return new RegisterDeliveryPersonResult(false, result.Errors.Select(e => e.Description).ToList());
    //        }

    //        if (!await roleManager.RoleExistsAsync("DeliveryPerson"))
    //        {
    //            await roleManager.CreateAsync(new IdentityRole("DeliveryPerson"));
    //        }

    //        var roleResult = await userManager.AddToRoleAsync(user, "DeliveryPerson");

    //        if (!roleResult.Succeeded)
    //        {
    //            return new RegisterDeliveryPersonResult(false, roleResult.Errors.Select(e => e.Description).ToList());
    //        }
    //        await deliveryPersonRepositry.AddAsync(user);

    //        return new RegisterDeliveryPersonResult(true, new());
    //    }
    //}
    public class RegisterDeliveryHandler : IRequestHandler<RegisterDeliveryPersonCommand, RegisterDeliveryPersonResult>
    {
        private readonly IIdentityService identityService;
        private readonly IDeliveryPersonRepositry deliveryPersonRepositry;

        public RegisterDeliveryHandler(IIdentityService identityService, IDeliveryPersonRepositry deliveryPersonRepositry)
        {
            this.identityService = identityService;
            this.deliveryPersonRepositry = deliveryPersonRepositry;
        }

        public async Task<RegisterDeliveryPersonResult> Handle(RegisterDeliveryPersonCommand command, CancellationToken cancellationToken)
        {
            var (success, errors) = await identityService.RegisterUserAsync(
                command.UserName,
                command.Email,
                command.Password,
                command.PhoneNumber,
                "DeliveryPerson");

            if (!success)
                return new RegisterDeliveryPersonResult(false, errors);

            var user = await identityService.GetUserByEmailAsync(command.Email);
            if (user != null)
            {
                await deliveryPersonRepositry.AddAsync(user);
            }

            return new RegisterDeliveryPersonResult(true, new());
        }
    }
}
