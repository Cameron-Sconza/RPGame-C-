namespace Library.Models
{
    public class Item
    {
        public string Name { get; set; } //  ID
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public string Description { get; set; }
    }

    public class Equipment : Item
    {
        public double AttackBonus { get; set; }
        public double DefenceBonus { get; set; }
        public int StrengthBonus { get; set; }
        public int DexterityBonus { get; set; }
        public int ConstitutionBonus { get; set; }
        public int IntellegenceBonus { get; set; }
        public string ItemType { get; set; }
    }
}
