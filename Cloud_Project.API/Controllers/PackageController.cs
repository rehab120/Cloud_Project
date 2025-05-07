using Cloud_Project.Application.Usecase.Commands.CreatePackage;
using Cloud_Project.Application.Usecase.Commands.DeletePackage;
using Cloud_Project.Application.Usecase.Commands.UpdatePackage;
using Cloud_Project.Application.Usecase.Queries.GetAllPackages;
using Cloud_Project.Application.Usecase.Queries.GetPackageById;
using Cloud_Project.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _mediator.Send(new GetAllPackagesQuery());
            return Ok(packages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(string id)
        {
            var package = await _mediator.Send(new GetPackageByIdQuery(id));
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage([FromBody] CreatePackageCommand command)
        {
            await _mediator.Send(command);
            return Ok("Created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(string id, [FromBody] Package package)
        {
            await _mediator.Send(new UpdatePackageCommand(id, package));
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(string id)
        {
            await _mediator.Send(new DeletePackageCommand(id));
            return Ok("Deleted");
        }
    }
}
