using Cloud_Project.Application.Interface;
using Cloud_Project.Application.Usecase.Queries.GetDeliveryById;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Usecase.Queries.GetDeliveryById
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
