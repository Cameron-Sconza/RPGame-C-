using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Character
    {
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intellegence { get; set; }
        public int Gold { get; set; }
        public double CurrentExp { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public double NextLevelExp { get; set; }
        public double CurrentHealthPoints { get; set; }
        public double MaxHealthPoints { get; set; }
        public string Name { get; set; }
        public string CharacterClass { get; set; }
        public List<string> Backpack { get; set; } //ItemIDs
        public string MainHandID { get; set; }
        public string OffHandID { get; set;}
        public string ArmourID { get; set;}
        public MainHand MainHand { get; set; }
        public OffHand OffHand { get; set; }
        public Armour Armour { get; set; }
        public Character()
        {
            List<string> Backpack = new List<string>();
        }
    }
}
