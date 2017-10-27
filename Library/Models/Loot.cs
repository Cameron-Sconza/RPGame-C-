using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
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
