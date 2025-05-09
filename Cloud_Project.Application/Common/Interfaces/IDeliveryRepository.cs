using Cloud_Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace Cloud_Project.Application.Common.Interfaces

{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetAllDeliveriesAsync();
        Task<List<Delivery>> GetAllAssignedDeliveriesAsync();
        Task<List<Delivery>> GetAllFinishedDeliveriesAsync();
        Task<Delivery> GetDeliveryByIdAsync(string id);
        Task<string> CreateDeliveryAsync(string merchantId);
        Task<bool> AddPackagesToDeliveryAsync(string id, List<Package> packages);

        Task<Delivery> UpdateDeliveryAsync(Delivery delivery);
        Task<bool> IsDeleteableDeliveryAsync(string id);
        Task<bool> MakeDeliveryDeleteableAsync(string id);
        Task<bool> DeleteDeliveryAsync(string id);
    }
}
