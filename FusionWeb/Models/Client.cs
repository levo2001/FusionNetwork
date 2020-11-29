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


        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        
        public string PhoneNumber { get; set; }        
    }
}
