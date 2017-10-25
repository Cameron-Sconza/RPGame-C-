using Library.Models;
using DataAccess;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Logic
    {
        public static Data data = new Data();
        public Player LoadGame()
        {
            Player Player = data.LoadGame();
            if (Player == null) { return null; }
            else { return Player; }
        }

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
                return Math.Ceiling(a: (((mercenary.Intellegence * 2) / 3) + 2) + (0.8 * mercenary.Level));
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

        public void SaveGame(Player Player)
        {
            data.SaveGame(Player);
        }

        public double Damage(double dealerAttack, double recieverDefence, bool crit)
        {
            if (crit)
            {
                dealerAttack *= 2.50;
            }
            if (dealerAttack > recieverDefence)
            {
                return Math.Ceiling(dealerAttack);
            }
            else if (dealerAttack < recieverDefence)
            {
                return Math.Ceiling(dealerAttack / 3.00);
            }
            else
            {
                return Math.Ceiling(dealerAttack / 1.15);
            }
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

        public int RNG()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);
            return new Random(result).Next(1, 100);
        }

        public Monster GetMonster(string monsterName)
        {
            return data.GetMonster().Where(m => m.Name == monsterName).FirstOrDefault();
        }

        public Item GrabItem(Item item)
        {
            return data.GetAllItems().Where(i => i == item).FirstOrDefault();
        }

        public List<Item> GetShopItemNames()
        {
            return data.GetAllItems();
        }
    }
}
