using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Cloud_Project.Domain.Entities
{
    public class Delivery
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        public List<Package> Packages { get; set; } = new List<Package>();

        public Status  StatusDelivery { get; set; } 

        public int? Merchant_id { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(Merchant_id))]
        public Merchant? Merchant { get; set; }

        public int? DeliveryPerson_id { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(Merchant_id))]
        public DeliveryPerson? DeliveryPerson { get; set; }

    }
}
