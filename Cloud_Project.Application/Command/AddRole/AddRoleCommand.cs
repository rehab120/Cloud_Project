using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Cloud_Project.Application.Command.AddRole;
namespace Cloud_Project.Application.Command.AddRole
{
    public record AddRoleCommand(string RoleName):IRequest<AddRoleResult>;
    public record AddRoleResult(bool Success, List<string> Errors);
}

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, AddRoleResult>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public AddRoleCommandHandler(RoleManager<IdentityRole> _roleManager)
    {
        this._roleManager = _roleManager;
        
    }
    public async Task<AddRoleResult> Handle (AddRoleCommand command,CancellationToken cancellationToken)
    {
        var role = new IdentityRole{Name=command.RoleName};
        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
            return new AddRoleResult(true, new List<string>());

        }
        return new AddRoleResult(false, result.Errors.Select(s => s.Description).ToList());

    }

}
