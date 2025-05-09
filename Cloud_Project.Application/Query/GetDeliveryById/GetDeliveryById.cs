using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Application.Query.GetDeliveryById;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Query.GetDeliveryById
{
    public record GetDeliveryByIdQuery(string Id) : IRequest<Delivery>;
}
public class GetDeliveryById : IRequestHandler<GetDeliveryByIdQuery, Delivery>
{
    private readonly IDeliveryRepository _deliveryRepository;
    
    public GetDeliveryById(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }
    
    public async Task<Delivery> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _deliveryRepository.GetDeliveryByIdAsync(request.Id);
    }
}
