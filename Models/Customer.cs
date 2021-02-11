using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class Customer
    {
        public int Customer_id { get; set; }
        [Required(ErrorMessage = "Not Empty")]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
