using System;
using System.Collections.Generic;
using Library.Models;
using BusinessLogic;
using System.Threading;
using System.Linq;

namespace RPGame_C_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool appIsRunning = true;
            do
            {
                int choice = BasicMenu("Welcome To RPGame C# Edition.\nPlease Make a Selection Below.\n\t1. New Game.\n\t2. Load Game.\n\t3. Quit Game.\n", 3);
                switch (choice)
                {
                    case (1):
                        NewGame();
                        break;
                    case (2):
                        LoadGame();
                        break;
                    case (3):
                        appIsRunning = false;
                        break;
                }
            } while (appIsRunning);
        }

        private static void LoadGame()
        {
            Player Player = Logic.PlayerLogic.LoadGame();
            if (Player != null)
            {
                PlayGame(Player);
            }
            else { Console.WriteLine("No Saved Data.\nReturning to Main Menu."); Thread.Sleep(1000); }
        }

        private static void NewGame()
        {
            Player player = new Player
            {
                Name = Prompt("What Is Your Name?"),
                Gold = 500
            };
            Console.WriteLine("You Will Be Redirected to Hire Your First Mercenary Shortly.\nWithout Any Mercenaries, You Will Not Be Able to Do Much.");
            Thread.Sleep(1000);
            player.Mercenaries.Add(HireMerc(player));
            PlayGame(player);
        }

        private static void PlayGame(Player p)
        {
            bool isPlaying = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = BasicMenu("What Would You Like to Do?\n\t1. Save.\n\t2. Mercenary Menu.\n\t3. Craft Menu.\n\t4. Shop Menu.\n\t5. Quit.\n", 5);
                switch (choice)
                {
                    case (1):
                        Logic.PlayerLogic.SaveGame(player);
                        break;
                    case (2):
                        player = MercMenu(player);
                        break;
                    case (3):
                        player = CraftingMenu(player);
                        break;
                    case (4):
                        player = ShopMenu(player);
                        break;
                    case (5):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
        }

        private static Player CraftingMenu(Player p)
        {
            bool isCrafting = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = BasicMenu("What Would You Like to Do?\n\t1. Craft Weapons.\n\t2. Craft Armour.\n\t3. Craft Potions.\n\t4. Craft Misc.\n\t5. Back.\n", 5);
                switch (choice)
                {
                    case (1):
                        player = CraftWeapon(player);
                        break;
                    case (2):
                        player = CraftArmour(player);
                        break;
                    case (3):
                        player = CraftPotion(player);
                        break;
                    case (4):
                        player = CraftMisc(player);
                        break;
                    case (5):
                        isCrafting = false;
                        break;
                }
            } while (isCrafting);
            return player;
        }

        private static Player CraftMisc(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player CraftPotion(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player CraftArmour(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player CraftWeapon(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player MercMenu(Player p)
        {
            bool isPlaying = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = BasicMenu("What Would You Like to Do?\n\t1. Hire Mercenary.\n\t2. Fire Mercenary.\n\t3. View Your Mercenaries.\n\t4. Send A Mercenary On A Quest.\n\t5. Order A Mercenary to Rest.\n\t6. Quit.\n", 6);
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
                        Prompt("Hit Enter to Leave.");
                        break;
                    case (4):
                        player = Quest(player);
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

        private static Player OrderedRest(Player p)
        {
            Player player = p;
            Mercenary merc = SelectMerc(player, "Please Select a Merc To Get Some Rest.");
            int index = player.Mercenaries.IndexOf(merc);
            merc.CurrentHealthPoints = merc.MaxHealthPoints;
            player.Mercenaries.RemoveAt(index);
            player.Mercenaries.Insert(index, merc);
            return player;
        }

        private static Player Quest(Player p)
        {
            Player player = p;
            bool isPlaying = true;
            do
            {
                int choice = BasicMenu("Select a Tier.\n\t1. Easy (Reccomended Level 1).\n\t2. Medium (Reccomended Level 10)\n\t3.  Hard (Reccomended Level 25)\n\t4. Leave.\n", 4);
                switch (choice)
                {
                    case (1):
                    case (2):
                    case (3):
                        player = Tier(player, choice);
                        break;
                    case (4):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
            return player;
        }

        private static Player Tier(Player p, int tier)
        {
            List<Quest> list = Logic.QuestLogic.GetAllQuests().Where(q => q.Tier == tier).ToList();
            int count = list.Count;
            ViewMerc(p);
            int choice = BasicMenu("Which Quest Would You Like to Sent A Mercenary to Complete?\nOr Enter a Number Up to 3 Higher Then Shown to Leave.", count + 3);
            try
            {
                Questing questing = Questing.Convert(list[choice - 1]);
                questing.Mercenary = SelectMerc(p, "Please Select a Merc To Send On This Quest.");
                if (questing.Mercenary == Mercenary.Empty)
                {
                    return p;
                }
                else
                {
                    return Logic.CombatLogic.Questing(p, questing);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return p;
            }
        }

        private static void ViewMerc(Player player)
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

        private static Mercenary SelectMerc(Player player, string text)
        {
            Console.Clear();
            ViewMerc(player);
            int choice = BasicMenu(text + "\nOr Enter A Number Up to 3 Higher then Shown to Cancel.\n", player.Mercenaries.Count + 3);
            try
            {
                return player.Mercenaries[choice - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return Mercenary.Empty;
            }
        }

        static Mercenary HireMerc(Player player)
        {
            int mercCost = 100 * ((int)Math.Pow(player.Mercenaries != null ? player.Mercenaries.Count : 0, 2));
            if (player.Gold > mercCost)
            {
                Console.Clear();
                int choice = BasicMenu("What is The Mercenaries Profession?\n\t1. Warrior.\n\t2. Rogue\n\t3. Mage\n", 3);
                Mercenary Merc = Mercenary.Empty;
                switch (choice)
                {
                    case (1):
                        Merc = new Mercenary()
                        {
                            Name = Prompt("What Shall Thier Name Be?"),
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
                            Name = Prompt("What Shall Thier Name Be?"),
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
                            Name = Prompt("What Shall Thier Name Be?"),
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

        private static Player ShopMenu(Player p)
        {
            bool isPlaying = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = BasicMenu("What Would You Like to Do?\n\t1. Buy.\n\t2. Sell.\n\t3. Quit.\n", 3);
                switch (choice)
                {
                    case (1):
                        player = BuyMenu(player);
                        break;
                    case (2):
                        player = SellMenu(player);
                        break;
                    case (3):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
            return player;
        }

        private static Player SellMenu(Player p)
        {
            Player player = p;
            bool isPlaying = true;
            do
            {
                int choice = BasicMenu("What Would You Like to Sell?\n\t1. Items.\n\t2. Equipment.\n\t3. Nevermind.", 3);
                switch (choice)
                {
                    case (1):
                        player = SellItems(player);
                        break;
                    case (2):
                        player = SellEquipment(player);
                        break;
                    case (3):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
            return player;
        }

        private static Player SellEquipment(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player SellItems(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player BuyMenu(Player p)
        {
            Player player = p;
            bool isPlaying = true;
            do
            {
                int choice = BasicMenu("What Would You Like to Sell?\n\t1. Items.\n\t2. Equipment.\n\t3. Nevermind.", 3);
                switch (choice)
                {
                    case (1):
                        player = BuyItems(player);
                        break;
                    case (2):
                        player = BuyEquipment(player);
                        break;
                    case (3):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
            return player;
        }

        private static Player BuyEquipment(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player BuyItems(Player player)
        {
            throw new NotImplementedException();
        }

        private static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        private static int BasicMenu(string text, int numOfChoices)
        {
            int choice;
            int fail = 0;
            do
            {
                Console.Write(text);
                if (fail > 3)
                {
                    Console.WriteLine("Please Use a Numerical Value.");
                }
                int.TryParse(Console.ReadLine(), out choice);
                fail++;
            } while (!(choice > 0 && choice <= numOfChoices) || choice == 0);
            return choice;
        }
    }
}
