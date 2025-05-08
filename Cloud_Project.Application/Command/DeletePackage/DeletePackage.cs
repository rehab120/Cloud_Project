using Cloud_Project.Application.Command.DeletePackage;
using Cloud_Project.Domain.Interface;
using MediatR;

namespace Cloud_Project.Application.Command.DeletePackage
{
    public record DeletePackageCommand(string PackageId) : IRequest<DeletePackageResult>;
    public record DeletePackageResult(bool Success, List<string> Errors);
}
public class DeletePackage : IRequestHandler<DeletePackageCommand, DeletePackageResult>
{
    private readonly IPackageRepository _packageRepository;
    
    public DeletePackage(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public async Task<DeletePackageResult> Handle(DeletePackageCommand command, CancellationToken cancellationToken)
    {
        var package = await _packageRepository.GetPackageByIdAsync(command.PackageId);
        if (package == null)
        {
            return new DeletePackageResult(false, new List<string> { "Package not found" });
        }

        var success = await _packageRepository.DeletePackageAsync(command.PackageId);
        if (success)
        {
            return new DeletePackageResult(true, new List<string>());
        }

        return new DeletePackageResult(false, new List<string> { "Failed to delete package" });
    }

}
