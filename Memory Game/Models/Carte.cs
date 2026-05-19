using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.Models
{
    public class Carte
    {
        public int Id { get; set; }
        public string Simbol { get; set; }
        public string Imagine { get; set; }
        public bool EsteIntoarsa { get; set; }
        public bool EsteGasita { get; set; }
    }
}
