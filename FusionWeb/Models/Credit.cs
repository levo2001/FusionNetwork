using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Credit
    {
        //
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CVV { get; set; }

    }
}
