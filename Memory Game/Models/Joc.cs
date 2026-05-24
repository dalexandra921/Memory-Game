using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.Models
{
    public class Joc
    {
        public List<Carte> listaCarti { get; set; } = new();
        public int scor { get; set; }
        public int mutari { get; set; }
        public int timpJoc { get; set; }
    }
}
