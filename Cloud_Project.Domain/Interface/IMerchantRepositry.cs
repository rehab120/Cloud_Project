using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cloud_Project.Domain.Interface

{
    public interface IMerchantRepositry
    {
         Task AddAsync(IdentityUser User);
    }
}
