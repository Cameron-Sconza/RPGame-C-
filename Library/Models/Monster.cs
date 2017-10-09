using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Monster
    {
        public double CurrentHealthPoints { get; set; }
        public double MaxHealthPoints { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public string Name { get; set; } // ID
    }
}
