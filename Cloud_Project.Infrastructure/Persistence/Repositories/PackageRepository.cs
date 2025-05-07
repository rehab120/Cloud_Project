using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Domain.Interface;
using Microsoft.EntityFrameworkCore;


namespace Cloud_Project.Infrastructure.Persistence.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly CloudDbContext _context;
        
        public PackageRepository(CloudDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Package>> GetAllPackagesAsync()
        {
            return await _context.Package.ToListAsync();
        }
        
        public async Task<Package> GetPackageByIdAsync(string id)
        {
            return await _context.Package.FindAsync(id);
        }
        
        public async Task<Package> CreatePackageAsync(Package package)
        {
            _context.Package.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }
        
        public async Task<Package> UpdatePackageAsync(Package package)
        {
            _context.Package.Update(package);
            await _context.SaveChangesAsync();
            return package;
        }
        
        public async Task<bool> DeletePackageAsync(string id)
        {
            var package = await GetPackageByIdAsync(id);
            if (package == null) return false;
            _context.Package.Remove(package);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
