using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Project.Domain.Entities
{
    public class DeliveryPerson
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Passsword { get; set; }    
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

       
    }
}
