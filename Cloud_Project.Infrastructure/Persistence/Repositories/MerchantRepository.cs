using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Domain.Interface;
using Microsoft.AspNetCore.Identity;

namespace Cloud_Project.Infrastructure.Persistence.Repositories
{
    public class MerchantRepository : IMerchantRepositry
    {
        private readonly CloudDbContext _context;
        public MerchantRepository(CloudDbContext _context)
        {
            this._context = _context;
        }

        public async Task AddAsync(IdentityUser User)
        {
            var merchant = new Merchant
            { 
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                Passsword = User.PasswordHash
            
            };

             _context.Merchant.Add(merchant);
            await _context.SaveChangesAsync();
        }

    }
}
