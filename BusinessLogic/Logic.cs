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
        public Character LoadGame()
        {
            Character character = data.LoadGameXml();
            if (character == null)
            {
                return null;
            }
            character.Attack = CalculateAttack(character.Strength, character.Dexterity, character.Intellegence, character.CharacterClass);
            character.Defence = CalculateDefence(character.Strength, character.Dexterity);
            character.MaxHealthPoints = CalculateMaxHealthPoints(character.Level, character.Strength, character.Dexterity, character.Constitution);
            character.CurrentHealthPoints = character.MaxHealthPoints;
            return character;
        }

        public double CalculateAttack(int strength, int dexterity, int intellegence, string characterClass)
        {
            if (characterClass == "Rogue")
            {
                return Math.Ceiling(a: (((dexterity * 2) / 3) + (strength * 0.5) + 2));
            }
            else if (characterClass == "Warrior")
            {
                return Math.Ceiling(a: (((strength * 2) / 3) + (dexterity * 0.5) + 3));
            }
            else if (characterClass == "Wizard")
            {
                return Math.Ceiling(a: (((intellegence * 2) / 3) + 2));
            }
            else { return 0; }
        }

        public double CalculateDefence(int strength, int dexterity)
        {
            return Math.Ceiling(a: (((strength + dexterity) / 1.5) + 2));
        }

        public double CalculateMaxHealthPoints(int level, int strength, int dexterity, int constitution)
        {
            return Math.Ceiling(a: ((((level * strength + (dexterity * level) / 4)) / 2) + (4 * constitution)));
        }

        public void SaveGame(Character character)
        {
            data.SaveGameXML(character);
        }

        public bool HitMiss()
        {
            if (RNG() > 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Crit()
        {
            if (RNG() < 25)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AIAction()
        {
            if (RNG() < 33)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Flee()
        {
            if (RNG() < 70)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double Damage(double dealerAttack, double recieverDefence, bool recieverIsDefending, bool crit)
        {
            if (recieverIsDefending)
            {
                recieverDefence *= 2.00;
            }
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
                return Math.Ceiling(dealerAttack / 2.00);
            }
            else
            {
                return Math.Ceiling(dealerAttack / 1.15);
            }
        }

        public Character Looting(Character character, Monster monster)
        {
            Character c = character;
            var monsterLoot = data.GetMonsterLoot().Where(ml => ml.MonsterID == monster.Name).FirstOrDefault();
            c.CurrentExp += monsterLoot.ExpGain;
            c.Gold += monsterLoot.GoldDrop;
            c.Backpack.Add(monsterLoot.ItemDropOneID);
            c.Backpack.Add(monsterLoot.ItemDropTwoID);
            c.Backpack.Add(monsterLoot.ItemDropThreeID);
            c.Backpack.Add(monsterLoot.ItemDropFourID);
            return c;
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

        public Item GrabItem(string itemName)
        {
            return data.GetAllItmes().Where(i => i.Name == itemName).FirstOrDefault();
        }

        public List<Item> GetShopItemNames()
        {
            return data.GetAllItmes();
        }
    }
}
