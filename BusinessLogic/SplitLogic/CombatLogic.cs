using Library.Models;
using System;
using System.Threading;
using System.Security.Cryptography;

namespace BusinessLogic.SplitLogic
{
    public class CombatLogic
    {
        public Player Questing(Player p, Questing q)
        {
            Player player = p;
            Questing quest = Combat(q);
            // Create a Save File For Quests, with a limit of so many quests possible at one time from there 
            // make a load method to be checked upon each running of the method, after that if there is nothing proceed
            // otherwise display the currecnt quests along with a menu choice to be made(ie another quest, complete a current one).
            // rebuild Questing Logic Accordingly, IE the list of active quests,
            // change the names of the methods so it doesnt cause evoking issues
            return p;
        }

        private Questing Combat(Questing q)
        {
            Mercenary mercenary = q.Mercenary;
            Questing quest = q;
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
                    quest.Mercenary = mercenary;
                    quest.Sucess = true;
                    continue;
                }
                else
                {
                    quest.Mercenary = mercenary;
                    quest.Sucess = false;
                    break;
                }
            }
            return null;
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
        private int RNG()
        {
            //Creates a New Instance of RNGCryptoServiceProvider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];
            //Uses RNGCryptoServiceProvider to Fill an Array Of Bytes
            rng.GetBytes(buffer);
            //Converts the Byte Array into an int
            int result = BitConverter.ToInt32(buffer, 0);
            //Creates a New Random Seeded with the int created from converting the bytes
            //Calls the .Next(int minValue, int maxValue) method to get a "Random" number
            //Returns the Value(int)
            return new Random(result).Next(1, 100);
        }
    }
}
