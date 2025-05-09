using Cloud_Project.Application.Command.DeleteDelivery;
using Cloud_Project.Application.Command.RequestDelivery;
using Cloud_Project.Application.Command.UpdateDeliveryStatus;
using Cloud_Project.Application.Query.GetAllAssignedDeliveries;
using Cloud_Project.Application.Query.GetAllDeliveries;
using Cloud_Project.Application.Query.GetAllFinishedDeliveries;
using Cloud_Project.Application.Query.GetDeliveryById;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cloud_Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryRequest([FromBody] List<string> packagesIds)
        {
            var merchantIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (merchantIdClaim == null)
            {
                return Unauthorized("Invalid Token: Merchant ID not found in claims");
            }
            var merchantId = merchantIdClaim.Value;
            if (string.IsNullOrEmpty(merchantId))
            {
                return BadRequest("Merchant ID is required");
            }

            //string merchantId = "M-1"; // For testing purposes, replace with actual merchant ID retrieval logic

            var result = await _mediator.Send(new AddDeliveryRequestCommand(merchantId, packagesIds));
            if (result.Success)
            {
                return Ok("Delivery request created successfully");
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _mediator.Send(new GetAllDeliveriesQuery());
            if (deliveries == null || deliveries.Count == 0)
            {
                return NotFound("No deliveries found");
            }
            return Ok(deliveries);
        }

        [HttpGet("assigned")]
        public async Task<IActionResult> GetAllAssignedDeliveries()
        {
            var deliveries = await _mediator.Send(new GetAllAssignedDeliveriesQuery());
            if (deliveries == null || deliveries.Count == 0)
            {
                return NotFound("No assigned deliveries found");
            }
            return Ok(deliveries);
        }

        [HttpGet("finished")]
        public async Task<IActionResult> GetAllFinishedDeliveries()
        {
            var deliveries = await _mediator.Send(new GetAllFinishedDeliveriesQuery());
            if (deliveries == null || deliveries.Count == 0)
            {
                return NotFound("No finished deliveries found");
            }
            return Ok(deliveries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryById(string id)
        {
            var delivery = await _mediator.Send(new GetDeliveryByIdQuery(id));
            if (delivery == null)
            {
                return NotFound("Delivery not found");
            }
            return Ok(delivery);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateDeliveryStatus(string id, [FromBody] string status)
        {
            var deliveryPersonIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (deliveryPersonIdClaim == null)
            {
                return Unauthorized("Invalid Token: Delivery Person ID not found in claims");
            }
            var deliveryPersonId = deliveryPersonIdClaim.Value;
            if (string.IsNullOrEmpty(deliveryPersonId))
            {
                return BadRequest("Delivery Person ID is required");
            }

            //string deliveryPersonId = "D-1"; // For testing purposes, replace with actual delivery person ID retrieval logic

            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status cannot be null or empty");
            }

            if (!Enum.TryParse(status, true, out Status deliveryStatus))
            {
                return BadRequest("Invalid status value");
            }

            var result = await _mediator.Send(new UpdateDeliveryStatusCommand (id, deliveryStatus, deliveryPersonId));
            if (result.Success)
            {
                return Ok("Delivery status updated successfully");
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryStatusAsync(string id)
        {
            var result = await _mediator.Send(new DeleteDeliveryCommand(id));
            if (result.Success)
            {
                return Ok("Delivery deleted successfully");
            }
            return BadRequest(result.Errors);
        }
    }
}
