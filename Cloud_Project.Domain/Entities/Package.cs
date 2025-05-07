using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Cloud_Project.Domain.Entities
{
    public class Package
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Size { get; set; }

        [Required]
        [Range(0.01, float.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public float Weight { get; set; }     // in kg

        [Required]
        public string Address { get; set; }

        public string? Delivery_id { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(Delivery_id))]
        public Delivery? Delivery { get; set; }
    }
}
