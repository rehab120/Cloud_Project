﻿using Cloud_Project.Application.Command.UpdateDeliveryStatus;
using Cloud_Project.Application.Common.Interfaces;

using Cloud_Project.Domain.Enums;
using MediatR;

namespace Cloud_Project.Application.Command.UpdateDeliveryStatus
{
    public record UpdateDeliveryStatusCommand(string DeliveryId, Status Status, string DeliveryPersonId) : IRequest<UpdateDeliveryStatusResult>;
    public record UpdateDeliveryStatusResult(bool Success, List<string> Errors);
}
public class UpdateDeliveryStatus : IRequestHandler<UpdateDeliveryStatusCommand, UpdateDeliveryStatusResult>
{
    private readonly IDeliveryRepository _deliveryRepository;
    
    public UpdateDeliveryStatus(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }
    
    public async Task<UpdateDeliveryStatusResult> Handle(UpdateDeliveryStatusCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.DeliveryPersonId))
        {
            return new UpdateDeliveryStatusResult(false, new List<string> { "Delivery Person ID is required" });
        }

        var delivery = await _deliveryRepository.GetDeliveryByIdAsync(request.DeliveryId);
        if (delivery == null)
        {
            return new UpdateDeliveryStatusResult(false, new List<string> { "Delivery not found" });
        }

        delivery.StatusDelivery = request.Status;
        delivery.DeliveryPerson_id = request.DeliveryPersonId;
        var result = await _deliveryRepository.UpdateDeliveryAsync(delivery);
        if(result == null)
        {
            return new UpdateDeliveryStatusResult(false, new List<string> { "Failed to update delivery status" });
        }
        return new UpdateDeliveryStatusResult(true, new List<string>());
    }
}