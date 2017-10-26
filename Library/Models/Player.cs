using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Player
    {
        public static readonly Player Empty;

        public string Name { get; set; }
        public int Gold { get; set; }
        public List<Item> ItemBackpack { get; set; }
        public List<Equipment> EquipmentBackpack { get; set; }
        public List<Mercenary> Mercenaries { get; set; }
        public Player()
        {
            ItemBackpack = new List<Item>();
            EquipmentBackpack = new List<Equipment>();
            Mercenaries = new List<Mercenary>();
        }
    }
}
