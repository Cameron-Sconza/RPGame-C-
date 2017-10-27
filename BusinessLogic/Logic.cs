using Library.Models;
using DataAccess;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading;

namespace BusinessLogic
{
    public class Logic
    {
        public static Data data = new Data();

        public void SaveGame(Player Player)
        {
            data.SaveGame(Player);
        }

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

        public List<Quest> GetAllQuests()
        {
            throw new NotImplementedException();
        }

        public Player Questing(Player p, Quest q)
        {
            Player player = p;
            Quest quest = Combat(q);
            //Place Logic for wait times here before battle.
            //Have the battle fall through and should end quickly, or i could remove travel times
            //If i remove the travel times there will be a Thread.Sleep(1000) between each action
            //Each Action Taken will be either Merc, or Monster
            //Once one of them dies end the fight and move oneto either the next mon or end the run.
            return p;
        }

        private Quest Combat(Quest q)
        {
            Mercenary mercenary = q.Mercenary;
            Quest quest = q;
            foreach (var monster in quest.Monsters)
            {
                do
                {
                    double mercDamageDelt = Damage(mercenary.Attack, monster.Defence, RNG() < 10 ? true : false);
                    double monDamageDelt = Damage(monster.Attack, mercenary.Defence, RNG() < 10 ? true : false);
                    monster.CurrentHealthPoints -= mercDamageDelt;
                    mercenary.CurrentHealthPoints -= monDamageDelt;
                    quest.ActionLog = LogOneRound(quest.ActionLog, mercenary.Name + " Delt " + mercDamageDelt + " Damage to " + monster.Name + ".",
                        mercenary.Name + " Took " + monDamageDelt + " Damage From " + monster.Name + ".",
                        mercenary.Name + " Current Health: " + mercenary.CurrentHealthPoints + "\n\t" + monster.Name + " Current Health: " + monster.CurrentHealthPoints);
                } while (monster.CurrentHealthPoints > 0 && mercenary.CurrentHealthPoints > 0);
                if (mercenary.CurrentHealthPoints < 0)
                {
                    q.Mercenary = mercenary;
                    quest.Sucess = false;
                    break;
                }
                else
                {
                    q.Mercenary = mercenary;
                    quest.Sucess = true;
                    break;
                }
            }
            return quest;
        }

        private ActionLog LogOneRound(ActionLog actionLog, string firstAction, string secondAction, string endOfRound)
        {
            Thread.Sleep(500);
            actionLog.Actions.Add(firstAction);
            actionLog.TimeStamp.Add(DateTime.Now);
            Thread.Sleep(500);
            actionLog.Actions.Add(secondAction);
            actionLog.TimeStamp.Add(DateTime.Now);
            Thread.Sleep(500);
            actionLog.Actions.Add("End Of Round Health.\n\t" + endOfRound);
            actionLog.TimeStamp.Add(DateTime.Now);
            return actionLog;
        }

        private double Damage(double dealerAttack, double recieverDefence, bool crit)
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
    }
}
