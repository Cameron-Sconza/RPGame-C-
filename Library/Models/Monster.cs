using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Monster : Loot
    {
        public double CurrentHealthPoints { get; set; }
        public double MaxHealthPoints { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public string Name { get; set; } // ID
    }

    public class Loot
    {
        public List<Item> Items { get; set; }
        public int GoldDrop { get; set; }
        public double ExpGain { get; set; }
        public Loot()
        {
            Items = new List<Item>();
        }
    }
}
