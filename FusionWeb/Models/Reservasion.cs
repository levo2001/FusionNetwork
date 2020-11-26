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
        public  string Date { get; set; }
        public  string Houre { get; set; }
        public Client NumOfDinners { get; set; }
        public string Nots { get; set; }


    }
}
