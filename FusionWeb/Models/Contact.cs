using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public Client InfoClient { get; set; }
        public string Content { get; set; }

    }
}
