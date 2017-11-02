using BusinessLogic;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGame_C_sharp.SplitProgram
{
    class MercenaryProgram
    {
        public Player MercMenu(Player p)
        {
            bool isPlaying = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = Program.BasicMenu("What Would You Like to Do?\n\t1. Hire Mercenary.\n\t2. Fire Mercenary.\n\t3. View Your Mercenaries.\n\t4. Send A Mercenary On A Quest.\n\t5. Order A Mercenary to Rest.\n\t6. Quit.\n", 6);
                switch (choice)
                {
                    case (1):
                        player.Mercenaries.Add(HireMerc(player));
                        break;
                    case (2):
                        player.Mercenaries.Remove(SelectMerc(player, "Please Select a Merc To Fire."));
                        break;
                    case (3):
                        ViewMerc(player);
                        Program.Prompt("Hit Enter to Leave.");
                        break;
                    case (4):
                        player = Program.QuestingProgram.Quest(player);
                        break;
                    case (5):
                        player = OrderedRest(player);
                        break;
                    case (6):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
            return player;
        }

        public Player OrderedRest(Player p)
        {
            Player player = p;
            Mercenary merc = SelectMerc(player, "Please Select a Merc To Get Some Rest.");
            int index = player.Mercenaries.IndexOf(merc);
            merc.CurrentHealthPoints = merc.MaxHealthPoints;
            player.Mercenaries.RemoveAt(index);
            player.Mercenaries.Insert(index, merc);
            return player;
        }

        public void ViewMerc(Player player)
        {
            Console.Clear();
            int i = 1;
            foreach (var merc in player.Mercenaries)
            {
                Console.WriteLine((i + 1) + ".\tName: " + merc.Name + "\tHeathPoints: " + merc.CurrentHealthPoints + '/' + merc.MaxHealthPoints +
                    "\nStrength: " + merc.Strength + "\tDexterity: " + merc.Dexterity + "\tIntellegence: " + merc.Intellegence +
                    "\nAttack: " + merc.Attack + "\tDefence: " + merc.Defence + "\tExp: " + merc.CurrentExp + '/' + merc.NextLevelExp + '\n');
                i++;
            }
        }

        public Mercenary SelectMerc(Player player, string text)
        {
            Console.Clear();
            ViewMerc(player);
            int choice = Program.BasicMenu(text + "\nOr Enter A Number Up to 3 Higher then Shown to Cancel.\n", player.Mercenaries.Count + 3);
            try
            {
                return player.Mercenaries[choice - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return Mercenary.Empty;
            }
        }

        public Mercenary HireMerc(Player player)
        {
            int mercCost = 100 * ((int)Math.Pow(player.Mercenaries != null ? player.Mercenaries.Count : 0, 2));
            if (player.Gold > mercCost)
            {
                Console.Clear();
                int choice = Program.BasicMenu("What is The Mercenaries Profession?\n\t1. Warrior.\n\t2. Rogue\n\t3. Mage\n", 3);
                Mercenary Merc = Mercenary.Empty;
                switch (choice)
                {
                    case (1):
                        Merc = new Mercenary()
                        {
                            Name = Program.Prompt("What Shall Thier Name Be?"),
                            Profession = "Warrior",
                            Level = 1,
                            Intellegence = 4,
                            Strength = 8,
                            Dexterity = 6,
                            Constitution = 7,
                            NextLevelExp = 25,
                            CurrentExp = 0,
                        };
                        break;
                    case (2):
                        Merc = new Mercenary()
                        {
                            Name = Program.Prompt("What Shall Thier Name Be?"),
                            Profession = "Rogue",
                            Level = 1,
                            Intellegence = 5,
                            Strength = 5,
                            Dexterity = 9,
                            Constitution = 6,
                            NextLevelExp = 25,
                            CurrentExp = 0,
                        };
                        break;
                    case (3):
                        Merc = new Mercenary()
                        {
                            Name = Program.Prompt("What Shall Thier Name Be?"),
                            Profession = "Mage",
                            Level = 1,
                            Intellegence = 13,
                            Strength = 3,
                            Dexterity = 4,
                            Constitution = 5,
                            NextLevelExp = 25,
                            CurrentExp = 0,
                        };
                        break;
                }
                Merc.Attack = Logic.StatsLogic.CalculateAttack(Merc);
                Merc.Defence = Logic.StatsLogic.CalculateDefence(Merc);
                Merc.MaxHealthPoints = Logic.StatsLogic.CalculateMaxHealthPoints(Merc);
                Merc.CurrentHealthPoints = Merc.MaxHealthPoints;
                return Merc;
            }
            else
            {
                Console.WriteLine("You Dont Have Enough Gold to Hire A Mercenary.");
                Thread.Sleep(1750);
                return null;
            }
        }

    }
}
