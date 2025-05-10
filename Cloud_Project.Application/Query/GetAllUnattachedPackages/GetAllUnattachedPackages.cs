using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Application.Query.GetAllUnattachedPackages;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Query.GetAllUnattachedPackages
{
    public record GetAllUnattachedPackagesQuery() : IRequest<List<Package>>;
}
public class GetAllUnattachedPackages : IRequestHandler<GetAllUnattachedPackagesQuery, List<Package>>
{
    private readonly IPackageRepository _packageRepository;
    
    public GetAllUnattachedPackages(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }
    
    public async Task<List<Package>> Handle(GetAllUnattachedPackagesQuery request, CancellationToken cancellationToken)
    {
        var packages = await _packageRepository.GetUnattachedToDeliveryPackagesAsync();
        return packages;
    }
}
