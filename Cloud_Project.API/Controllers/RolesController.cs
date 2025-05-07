using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cloud_Project.Application.Command.AddRole;

namespace Cloud_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator mediator;
        public RolesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            var result = await mediator.Send(command);
            if(result.Success)
            {
                return Ok("role Added Sucessfully");
            }
            return BadRequest(result.Errors);
        }
    }
}
