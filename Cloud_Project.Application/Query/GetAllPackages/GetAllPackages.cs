using Cloud_Project.Domain.Interface;
using Cloud_Project.Application.Query.GetAllPackages;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Query.GetAllPackages
{
    public record GetAllPackagesQuery() : IRequest<List<Package>>;
}
public class GetAllPackages : IRequestHandler<GetAllPackagesQuery, List<Package>>
{
    private readonly IPackageRepository _packageRepository;

    public GetAllPackages(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<List<Package>> Handle(GetAllPackagesQuery request, CancellationToken cancellationToken)
    {
        var packages = await _packageRepository.GetAllPackagesAsync();
        return packages;
    }
}
