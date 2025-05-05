using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Enums;

namespace Cloud_Project.Domain.Entities
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Weight { get; set; }
        public string Size { get; set; }
        public Status  StatusDelivery { get; set; } 

        public int Merchant_id { get; set; }

        [ForeignKey(nameof(Merchant_id))]
        public Merchant Merchant { get; set; }

    }
}
