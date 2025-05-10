using Cloud_Project.Application.Common.Enums;
using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Cloud_Project.Infrastructure.Persistence.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {

        private readonly CloudDbContext _context;
        private readonly IIdGenerator _idGenerator;

        public DeliveryRepository(CloudDbContext context, IIdGenerator idGenerator)
        {
            _context = context;
            _idGenerator = idGenerator;
        }

        public async Task<List<Delivery>> GetAllDeliveriesAsync()
        {
            return await _context.Delivery.ToListAsync();
        }

        public async Task<List<Delivery>> GetAllAssignedDeliveriesAsync()
        {
            return await _context.Delivery.Where(d => d.StatusDelivery == Status.Pending || d.StatusDelivery == Status.Picked).ToListAsync();
        }

        public async Task<List<Delivery>> GetAllFinishedDeliveriesAsync()
        {
            return await _context.Delivery.Where(d => d.StatusDelivery == Status.Delivered).ToListAsync();
        }

        public async Task<Delivery> GetDeliveryByIdAsync(string id)
        {
            return await _context.Delivery.FindAsync(id);
        }

        public async Task<List<Delivery>> GetAllDeliveriesByMerchantIdAsync(string id)
        {
            return await _context.Delivery.Where(d => d.Merchant_id == id).Include(d => d.DeliveryPerson).ToListAsync();
        }

        public async Task<List<Delivery>> GetAllDeliveriesByDeliveryPersonIdAsync(string id)
        {
            return await _context.Delivery.Where(d => d.DeliveryPerson_id == id || d.DeliveryPerson_id == null).Include(d => d.Merchant).ToListAsync();
        }

        public async Task<string> CreateDeliveryAsync(string merchantId)
        {
            var delivery = new Delivery
            {
                Id = _idGenerator.GenerateId<Delivery>(ModelPrefix.Delivery),
                StatusDelivery = Status.Pending,
                Merchant_id = merchantId,
            };

            await _context.Delivery.AddAsync(delivery);
            await _context.SaveChangesAsync();

            return delivery.Id;
        }

        public async Task<bool> AddPackagesToDeliveryAsync(string id, List<Package> packages)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var delivery = _context.Delivery.Find(id);
                if (delivery == null)
                {
                    return false;
                }

                foreach (var package in packages)
                {
                    if (package.DeliveryId != null)
                    {
                        var lastDelivery = _context.Delivery.OrderByDescending(d => d.Id).FirstOrDefault();
                        _context.Delivery.Remove(lastDelivery);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return false;
                    }
                    package.DeliveryId = id;
                    _context.Package.Update(package);
                }

                delivery.Packages = packages;
                _context.Delivery.Update(delivery);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<Delivery> UpdateDeliveryAsync(Delivery delivery)
        {
            _context.Delivery.Update(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }

        public async Task<bool> IsDeleteableDeliveryAsync(string id)
        {
            var delivery = await GetDeliveryByIdAsync(id);
            if (delivery == null) return false;
            List<Package> packages = await _context.Package.Where(p => p.DeliveryId == id).ToListAsync();
            if (packages.Any())
            {
                return false;
            }
            return true;
        }

        public async Task<bool> MakeDeliveryDeleteableAsync(string id)
        {
            List<Package> packages = await _context.Package.Where(p => p.DeliveryId == id).ToListAsync();
            foreach (var package in packages)
            {
                package.DeliveryId = null;
                _context.Package.Update(package);
            }
            return true;
        }

        public async Task<bool> DeleteDeliveryAsync(string id)
        {
            var delivery = await GetDeliveryByIdAsync(id);
            if (delivery == null) return false;
            if (!IsDeleteableDeliveryAsync(id).Result)
            {
                await MakeDeliveryDeleteableAsync(id);
                if (!MakeDeliveryDeleteableAsync(id).Result)
                {
                    return false;
                }
            }
            _context.Delivery.Remove(delivery);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
