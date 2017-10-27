using System.Collections.Generic;

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
}
