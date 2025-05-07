using Cloud_Project.Domain.Interface;
using Cloud_Project.Application.Query.GetAllFinishedDeliveries;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Query.GetAllFinishedDeliveries
{
    public record GetAllFinishedDeliveriesQuery() : IRequest<List<Delivery>>;
}
public class GetAllFinishedDeliveries : IRequestHandler<GetAllFinishedDeliveriesQuery, List<Delivery>>
{
    private readonly IDeliveryRepository _deliveryRepository;
    
    public GetAllFinishedDeliveries(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }
    
    public async Task<List<Delivery>> Handle(GetAllFinishedDeliveriesQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryRepository.GetAllFinishedDeliveriesAsync();
    }
}
