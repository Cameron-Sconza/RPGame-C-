using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.SplitLogic
{
    public class StatsLogic
    {
        public double CalculateAttack(Mercenary mercenary)
        {
            if (mercenary.Profession == "Rogue")
            {
                return Math.Ceiling(a: (((mercenary.Dexterity * 2) / 3) + (mercenary.Strength * 0.5) + 2) + (0.6 * mercenary.Level));
            }
            else if (mercenary.Profession == "Warrior")
            {
                return Math.Ceiling(a: (((mercenary.Strength * 2) / 3) + (mercenary.Dexterity * 0.5) + 3) + (0.4 * mercenary.Level));
            }
            else if (mercenary.Profession == "Mage")
            {
                return Math.Ceiling(a: (((mercenary.Intellegence * 2.75) / 3) + 4) + (0.8 * mercenary.Level));
            }
            else { return 0; }
        }

        public double CalculateDefence(Mercenary mercenary)
        {
            return Math.Ceiling(a: (((mercenary.Strength + mercenary.Dexterity) / 1.5) + 2));
        }

        public double CalculateMaxHealthPoints(Mercenary mercenary)
        {
            return Math.Ceiling(a: ((((mercenary.Level * mercenary.Strength + (mercenary.Dexterity * mercenary.Level) / 4)) / 2) + (4 * mercenary.Constitution)));
        }



        private Mercenary LevelUp(Mercenary mercenary)
        {
            Mercenary merc = mercenary;
            merc.Level++;
            merc.Constitution++;
            if (merc.Level % 15 == 0)
            {
                merc.Strength++;
                merc.Dexterity++;
                merc.Intellegence++;
            }
            if (merc.Level % 10 == 0)
            {
                merc.Constitution++;
                switch (merc.Profession)
                {
                    case ("Warrior"):
                        merc.Strength++;
                        break;
                    case ("Rogue"):
                        merc.Dexterity++;
                        break;
                    case ("Mage"):
                        merc.Intellegence++;
                        break;
                }
            }
            if (merc.Level % 5 == 0)
            {
                switch (merc.Profession)
                {
                    case ("Warrior"):
                        merc.Strength++;
                        break;
                    case ("Rogue"):
                        merc.Dexterity++;
                        break;
                    case ("Mage"):
                        merc.Intellegence++;
                        break;
                }
            }
            merc.Attack = CalculateAttack(merc);
            merc.Defence = CalculateDefence(merc);
            merc.MaxHealthPoints = CalculateMaxHealthPoints(merc);
            return merc;
        }
    }
}
