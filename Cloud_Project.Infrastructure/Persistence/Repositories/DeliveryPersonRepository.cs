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
    public class DeliveryPersonRepository : IDeliveryPersonRepositry
    {
        private readonly CloudDbContext _context;
        public DeliveryPersonRepository(CloudDbContext _context)
        {
            this._context = _context;
        }

        public async Task AddAsync(IdentityUser deliveryPerson)
        {
            var person = new DeliveryPerson
            {
                Id = deliveryPerson.Id,
                UserName = deliveryPerson.UserName,
                Email = deliveryPerson.Email,
                Passsword = deliveryPerson.PasswordHash,
                PhoneNumber = deliveryPerson.PhoneNumber
            };

            _context.DeliveryPersons.Add(person);
            await _context.SaveChangesAsync(); // Make it async
        }

    }
}
