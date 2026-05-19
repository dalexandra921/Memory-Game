using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.Models
{
    public class Joc
    {
        public List<Carte> ListaCarti { get; set; }
        public int Scor { get; set; }
        public int Mutari { get; set; }
        public int TimpJoc { get; set; }
    }
}
