using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public ICollection<Plate> Plates{ get; set; }
        public int NumPlates{ get; set; }
        public int Total{ get; set; }
    }
}
