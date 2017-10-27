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
        public Quest()
        {
            Monsters = new List<Monster>();
        }
    }

    public class Questing : Quest
    {
        public static Questing Convert(Quest quest)
        {
            return new Questing()
            {
                Name = quest.Name,
                RequiredLevel = quest.RequiredLevel,
                Tier = quest.Tier,
                Difficulty = quest.Difficulty,
                TravelTime = quest.TravelTime,
                GoldCost = quest.GoldCost,
                Monsters = quest.Monsters
            };
        }

        public bool Sucess { get; set; }
        public Mercenary Mercenary { get; set; }
        public ActionLog ActionLog { get; set; }
    }
}