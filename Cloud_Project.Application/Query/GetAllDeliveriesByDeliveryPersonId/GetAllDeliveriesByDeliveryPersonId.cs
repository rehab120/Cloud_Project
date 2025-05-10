using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Application.Query.GetAllDeliveriesByDeliveryPersonId;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Query.GetAllDeliveriesByDeliveryPersonId
{
    public record GetAllDeliveriesByDeliveryPersonIdQuery(string DeliveryPersonId) : IRequest<List<Delivery>>;
}
public class GetAllDeliveriesByDeliveryPersonId : IRequestHandler<GetAllDeliveriesByDeliveryPersonIdQuery, List<Delivery>>
{
    private readonly IDeliveryRepository _deliveryRepository;

    public GetAllDeliveriesByDeliveryPersonId(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }

    public async Task<List<Delivery>> Handle(GetAllDeliveriesByDeliveryPersonIdQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryRepository.GetAllDeliveriesByDeliveryPersonIdAsync(request.DeliveryPersonId);
    }
}
