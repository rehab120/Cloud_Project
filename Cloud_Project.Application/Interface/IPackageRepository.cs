using Cloud_Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Project.Application.Interface
{
    public interface IPackageRepository
    {
        Task<List<Package>> GetAllPackagesAsync();
        Task<Package> GetPackageByIdAsync(string id);
        Task<Package> CreatePackageAsync(Package package);
        Task<Package> UpdatePackageAsync(Package package);
        Task<bool> DeletePackageAsync(string id);

    }
}
