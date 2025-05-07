using Cloud_Project.Application.Interface;
using Cloud_Project.Application.Usecase.Queries.GetAllAssignedDeliveries;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Usecase.Queries.GetAllAssignedDeliveries
{
    public record GetAllAssignedDeliveriesQuery() : IRequest<List<Delivery>>;
}
public class GetAllAssignedDeliveries : IRequestHandler<GetAllAssignedDeliveriesQuery, List<Delivery>>
{
    private readonly IDeliveryRepository _deliveryRepository;
    
    public GetAllAssignedDeliveries(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }
    
    public async Task<List<Delivery>> Handle(GetAllAssignedDeliveriesQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryRepository.GetAllAssignedDeliveriesAsync();
    }
}