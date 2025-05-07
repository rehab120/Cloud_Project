using Cloud_Project.Application.Command.DeleteDelivery;
using Cloud_Project.Application.Command.RequestDelivery;
using Cloud_Project.Application.Command.UpdateDeliveryStatus;
using Cloud_Project.Application.Query.GetAllAssignedDeliveries;
using Cloud_Project.Application.Query.GetAllFinishedDeliveries;
using Cloud_Project.Application.Query.GetDeliveryById;
using Cloud_Project.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryRequest([FromBody] AddDeliveryRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok("Delivery request created successfully");
            }
            return BadRequest(result.Errors);
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
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status cannot be null or empty");
            }

            if (!Enum.TryParse(status, true, out Status deliveryStatus))
            {
                return BadRequest("Invalid status value");
            }

            var result = await _mediator.Send(new UpdateDeliveryStatusCommand (id, deliveryStatus));
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
