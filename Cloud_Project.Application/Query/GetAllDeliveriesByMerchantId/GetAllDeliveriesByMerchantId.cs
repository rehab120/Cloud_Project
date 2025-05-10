using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Application.Query.GetAllDeliveriesByMerchantId;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Query.GetAllDeliveriesByMerchantId
{
    public record GetAllDeliveriesByMerchantIdQuery(string MerchantId) : IRequest<List<Delivery>>;
}
public class GetAllDeliveriesByMerchantId : IRequestHandler<GetAllDeliveriesByMerchantIdQuery, List<Delivery>>
{
    private readonly IDeliveryRepository _deliveryRepository;
    
    public GetAllDeliveriesByMerchantId(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }
    
    public async Task<List<Delivery>> Handle(GetAllDeliveriesByMerchantIdQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryRepository.GetAllDeliveriesByMerchantIdAsync(request.MerchantId);
    }
}
