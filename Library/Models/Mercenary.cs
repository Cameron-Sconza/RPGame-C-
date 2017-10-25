using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Mercenary
    {
        public static readonly Mercenary Empty;

        public string Name { get; set; }
        public string Profession { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intellegence { get; set; }
        public double CurrentExp { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public double NextLevelExp { get; set; }
        public double CurrentHealthPoints { get; set; }
        public double MaxHealthPoints { get; set; }
        public Equipment MainHand { get; set; }
        public Equipment OffHand { get; set; }
        public Equipment Armour { get; set; }
    }
}
