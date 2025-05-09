using Cloud_Project.Application.Query.GetAllDeliveries;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Domain.Interface;
using MediatR;

namespace Cloud_Project.Application.Query.GetAllDeliveries
{
    public record GetAllDeliveriesQuery() : IRequest<List<Delivery>>;
}
public class GetAllDeliveries : IRequestHandler<GetAllDeliveriesQuery, List<Delivery>>
{
    private readonly IDeliveryRepository _deliveryRepository;

    public GetAllDeliveries(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }

    public async Task<List<Delivery>> Handle(GetAllDeliveriesQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryRepository.GetAllDeliveriesAsync();
    }
}
