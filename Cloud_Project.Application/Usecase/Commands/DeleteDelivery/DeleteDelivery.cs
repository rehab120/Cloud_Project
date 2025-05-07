using Cloud_Project.Application.Interface;
using Cloud_Project.Application.Usecase.Commands.DeleteDelivery;
using MediatR;

namespace Cloud_Project.Application.Usecase.Commands.DeleteDelivery
{
    public record DeleteDeliveryCommand(string DeliveryId) : IRequest<DeleteDeliveryResult>;
    public record DeleteDeliveryResult(bool Success, List<string> Errors);
}
public class DeleteDelivery : IRequestHandler<DeleteDeliveryCommand, DeleteDeliveryResult>
{
    private readonly IDeliveryRepository _deliveryRepository;
    
    public DeleteDelivery(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }
    
    public async Task<DeleteDeliveryResult> Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryRepository.GetDeliveryByIdAsync(request.DeliveryId);
        if (delivery == null)
        {
            return new DeleteDeliveryResult(false, new List<string> { "Delivery not found" });
        }
        var result = await _deliveryRepository.DeleteDeliveryAsync(request.DeliveryId);
        if (!result)
        {
            return new DeleteDeliveryResult(false, new List<string> { "Failed to delete delivery" });
        }
        return new DeleteDeliveryResult(result, new List<string>());
    }
}