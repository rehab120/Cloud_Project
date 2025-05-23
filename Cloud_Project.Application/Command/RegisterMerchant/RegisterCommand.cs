﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cloud_Project.Application.Command.RegisterMerchant
{
    public record RegisterCommand(string UserName, string Email, string Password) : IRequest<RegisterResult>;
    public record RegisterResult(bool Success, List<string> Errors);

    //public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
    //{
    //    private readonly UserManager<IdentityUser> userManager;
    //    private readonly RoleManager<IdentityRole> roleManager;
    //    private readonly IMerchantRepositry merchantRepositry;

    //    public RegisterHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMerchantRepositry merchantRepositry)
    //    {
    //        this.userManager = userManager;
    //        this.roleManager = roleManager;
    //        this.merchantRepositry = merchantRepositry;
    //    }

    //    public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    //    {
    //        var user = new IdentityUser
    //        {
    //            UserName = command.UserName,
    //            Email = command.Email,
    //        };

    //        var result = await userManager.CreateAsync(user, command.Password);
    //        if (!result.Succeeded)
    //        {
    //            return new RegisterResult(false, result.Errors.Select(e => e.Description).ToList());
    //        }

    //        if (!await roleManager.RoleExistsAsync("Merchant"))
    //        {
    //            await roleManager.CreateAsync(new IdentityRole("Merchant"));
    //        }

    //        var roleResult = await userManager.AddToRoleAsync(user, "Merchant");
    //        if (!roleResult.Succeeded)
    //        {
    //            return new RegisterResult(false, roleResult.Errors.Select(e => e.Description).ToList());
    //        }
    //        merchantRepositry.AddAsync(user);

    //        return new RegisterResult(true, new());
    //    }
    //}
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly IIdentityService identityService;
        private readonly IMerchantRepositry merchantRepositry;

        public RegisterHandler(IIdentityService identityService, IMerchantRepositry merchantRepositry)
        {
            this.identityService = identityService;
            this.merchantRepositry = merchantRepositry;
        }

        public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var (success, errors) = await identityService.RegisterUserAsync(command.UserName, command.Email, command.Password, "Merchant");

            if (!success)
                return new RegisterResult(false, errors);

            var user = await identityService.GetUserByEmailAsync(command.Email);
            if (user != null)
            {
                await merchantRepositry.AddAsync(user);
            }

            return new RegisterResult(true, new());
        }
    }
}
