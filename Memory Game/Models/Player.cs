using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.Models
{
    public class Player
    {
        public string Username { get; set; }
        public int BestScore { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public string BestTime { get; set; }
        public int RankedScore { get; set; }
        public string Difficulty { get; set; }
    }
}
