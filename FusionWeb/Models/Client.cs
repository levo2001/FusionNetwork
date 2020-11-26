using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Client
    {
        public string Name { get; set; }
        public string  Email { get; set; }
        public string  Address { get; set; }
        public string  City { get; set; }
        public string  PhoneNumber { get; set; }
        public Credit  Credit { get; set; }
    }
}
