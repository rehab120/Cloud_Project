using Cloud_Project.Application.Command.RequestDelivery;
using Cloud_Project.Application.Common.Enums;
using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Domain.Enums;
using MediatR;

namespace Cloud_Project.Application.Command.RequestDelivery
{
    public record AddDeliveryRequestCommand(string MerchantId, List<string> PackagesIds) : IRequest<AddDeliveryRequestResult>;
    public record AddDeliveryRequestResult(bool Success, List<string> Errors);
}
public class CreateDeliveryRequest : IRequestHandler<AddDeliveryRequestCommand, AddDeliveryRequestResult>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IPackageRepository _packageRepository;

    public CreateDeliveryRequest(IDeliveryRepository deliveryRepository, IPackageRepository packageRepository)
    {
        _deliveryRepository = deliveryRepository;
        _packageRepository = packageRepository;
    }

    public async Task<AddDeliveryRequestResult> Handle(AddDeliveryRequestCommand command, CancellationToken cancellationToken)
    {
        var merchantId = command.MerchantId;
        if (string.IsNullOrEmpty(merchantId))
        {
            return new AddDeliveryRequestResult(false, new List<string> { "Merchant ID is required" });
        }

        var PackagesIds = command.PackagesIds;
        if (PackagesIds == null || PackagesIds.Count == 0)
        {
            return new AddDeliveryRequestResult(false, new List<string> { "No packages provided" });
        }

        var packages = new List<Package>();
        foreach (var packageId in PackagesIds)
        {
            var package = await _packageRepository.GetPackageByIdAsync(packageId);
            if (package == null)
            {
                return new AddDeliveryRequestResult(false, new List<string> { $"Package with ID {packageId} not found" });
            }
            packages.Add(package);
        }

        var deliveryId = await _deliveryRepository.CreateDeliveryAsync(merchantId);
        var result = await _deliveryRepository.AddPackagesToDeliveryAsync(deliveryId, packages);
        if (!result)
        {
            return new AddDeliveryRequestResult(false, new List<string> { "Failed to create delivery request" });
        }
        return new AddDeliveryRequestResult(true, new List<string>());
    }
}

