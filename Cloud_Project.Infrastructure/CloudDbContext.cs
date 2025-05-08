using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Entities;
using Cloud_Project.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cloud_Project.Infrastructure
{
    public class CloudDbContext : IdentityDbContext<IdentityUser>
    {
        public CloudDbContext() : base() { }

        public CloudDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Package> Package { get; set; }

        
    }
}
