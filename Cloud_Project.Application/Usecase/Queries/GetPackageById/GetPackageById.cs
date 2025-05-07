using Cloud_Project.Application.Interface;
using Cloud_Project.Application.Usecase.Queries.GetPackageById;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Usecase.Queries.GetPackageById
{
    public record GetPackageByIdQuery(string Id) : IRequest<Package>;
}
public class GetPackageById : IRequestHandler<GetPackageByIdQuery, Package>
{
    private readonly IPackageRepository _packageRepository;
    
    public GetPackageById(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }
    
    public async Task<Package> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository.GetPackageByIdAsync(request.Id);
        return package;
    }
}