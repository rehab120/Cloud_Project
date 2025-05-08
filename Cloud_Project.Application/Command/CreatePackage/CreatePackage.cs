using Cloud_Project.Domain.Entities;
using MediatR;
using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Application.Common.Enums;
using Cloud_Project.Application.Command.CreatePackage;
using Cloud_Project.Domain.Interface;


namespace Cloud_Project.Application.Command.CreatePackage
{
    public record CreatePackageCommand(string Size, float Weight, string Address) : IRequest<CreatePackageResult>;
    public record CreatePackageResult(bool Success, List<string> Errors);
}

public class CreatePackage : IRequestHandler<CreatePackageCommand, CreatePackageResult>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IIdGenerator _idGenerator;

    public CreatePackage(IPackageRepository packageRepository, IIdGenerator idGenerator)
    {
        _packageRepository = packageRepository;
        _idGenerator = idGenerator;
    }

    public async Task<CreatePackageResult> Handle(CreatePackageCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.Size) || command.Weight <= 0 || string.IsNullOrEmpty(command.Address))
        {
            return new CreatePackageResult(false, new List<string> { "Invalid package details" });
        }

        var package = new Package
        {
            Id = _idGenerator.GenerateId<Package>(ModelPrefix.Package),
            Size = command.Size,
            Weight = command.Weight,
            Address = command.Address


        };
        var result = await _packageRepository.CreatePackageAsync(package);
        if (result != null)
        {
            return new CreatePackageResult(true, new List<string>());
        }
        return new CreatePackageResult(false, new List<string> { "Failed to create package"});
    }
}