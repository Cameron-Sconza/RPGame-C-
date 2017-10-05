using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class OffHand
    {
        public double AttackBonus { get; set; }
        public double DefenceBonus { get; set; }
        public int StrengthBonus { get; set; }
        public int DexterityBonus { get; set; }
        public int ConstitutionBonus { get; set; }
        public int IntellegenceBonus { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        public string Name { get; set; }
    }
}
