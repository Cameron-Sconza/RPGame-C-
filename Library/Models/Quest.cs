using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Quest : Loot
    {
        public string Name { get; set; } // ID
        public int RequiredLevel { get; set; }
        public int Tier { get; set; }
        public int Difficulty { get; set; }
        public long TravelTime { get; set; }
        public int GoldCost { get; set; }
        public List<Monster> Monsters { get; set; }
    }
}