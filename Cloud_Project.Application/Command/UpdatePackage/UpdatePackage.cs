using Cloud_Project.Application.Command.UpdatePackage;
using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Domain.Entities;
using MediatR;

namespace Cloud_Project.Application.Command.UpdatePackage
{
    public record UpdatePackageCommand(string Id, Package updatedPackage) : IRequest<UpdatePackageResult>;
    public record UpdatePackageResult(bool Success, List<string> Errors);
}
public class UpdatePackage : IRequestHandler<UpdatePackageCommand, UpdatePackageResult>
{
    private readonly IPackageRepository _packageRepository;
    
    public UpdatePackage(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<UpdatePackageResult> Handle(UpdatePackageCommand command, CancellationToken cancellationToken)
    {
        var package = await _packageRepository.GetPackageByIdAsync(command.Id);
        if (package == null)
        {
            return new UpdatePackageResult(false, new List<string> { "Package not found" });
        }

        var updated = command.updatedPackage;

        if (string.IsNullOrEmpty(updated.Size) || updated.Weight <= 0 || string.IsNullOrEmpty(updated.Address))
        {
            return new UpdatePackageResult(false, new List<string> { "Invalid package details" });
        }

        // Update only specific fields (to avoid losing data)
        package.Size = updated.Size;
        package.Weight = updated.Weight;
        package.Address = updated.Address;

        await _packageRepository.UpdatePackageAsync(package);

        return new UpdatePackageResult(true, new List<string>());
    }
}

