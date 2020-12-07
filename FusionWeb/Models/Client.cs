using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Client
    {
      public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        
        public string City { get; set; }

        //  public ICollection<Reservasion> Reservasions { get; set; }
       // public ICollection<Dish> Dishes { get; set; }
       
        [Required]
        [Phone]
        [Display(Name ="מס טלפון")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }        
    
    }
}
