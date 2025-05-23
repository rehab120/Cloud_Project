﻿using Cloud_Project.Application.Command.CreatePackage;
using Cloud_Project.Application.Command.DeletePackage;
using Cloud_Project.Application.Command.UpdatePackage;
using Cloud_Project.Application.Query.GetAllPackages;
using Cloud_Project.Application.Query.GetAllUnattachedPackages;
using Cloud_Project.Application.Query.GetPackageById;
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

        [HttpGet("unattached")]
        public async Task<IActionResult> GetUnattachedPackages()
        {
            var packages = await _mediator.Send(new GetAllUnattachedPackagesQuery());
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
            var result = await _mediator.Send(new DeletePackageCommand(id));

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok("Deleted");
        }

    }
}
