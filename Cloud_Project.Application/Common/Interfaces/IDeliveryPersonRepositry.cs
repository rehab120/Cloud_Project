using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Entities;
using Microsoft.AspNetCore.Identity;

 namespace Cloud_Project.Application.Common.Interfaces

{
    public interface IDeliveryPersonRepositry
    {
        Task AddAsync(IdentityUser deliveryPerson);

    }
}
