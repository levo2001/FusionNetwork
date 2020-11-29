using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FusionWeb.Models
{
    public class Reservasion
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public  DateTime DateTime { get; set; }
        public int NumOfDinners { get; set; }
        public string Note { get; set; }


    }
}
