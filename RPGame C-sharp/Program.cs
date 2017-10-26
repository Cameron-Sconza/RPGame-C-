using System;
using System.Collections.Generic;
using Library.Models;
using BusinessLogic;
using System.Threading;

namespace RPGame_C_sharp
{
    class Program
    {
        public static Logic logic = new Logic();
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
            Player Player = logic.LoadGame();
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
                        logic.SaveGame(player);
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
                int choice = BasicMenu("What Would You Like to Do?\n\t1. Craft Weapons.\n\t2. Craft Armour.\n\t3. Craft Potions.\n\t4. Back.\n", 4);
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
                        isCrafting = false;
                        break;
                }
            } while (isCrafting);
            return player;
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
                        player.Mercenaries.Remove(FireMerc(player));
                        break;
                    case (3):
                        ViewMerc(player);
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
            Console.Clear();
            for (int i = 0; i < player.Mercenaries.Count; i++)
            {
                Console.WriteLine((i + 1) + ".\tName: " + player.Mercenaries[i].Name + "\n\tProfession: " + player.Mercenaries[i].Profession + "\n\tLevel: " + player.Mercenaries[i].Level);
            }
            int choice = BasicMenu("Please Select a Merc To Get Some Rest.\nOr Enter A Number Up to 3 Higher then Shown to Cancel.\n", player.Mercenaries.Count + 3);
            try
            {
                choice--;
                player.Mercenaries[choice].CurrentHealthPoints = player.Mercenaries[choice].MaxHealthPoints;
                return player;
            }
            catch (ArgumentOutOfRangeException)
            {
                return player;
            }
        }

        private static Player Quest(Player player)
        {
            throw new NotImplementedException();
        }

        private static void ViewMerc(Player player)
        {
            foreach (var merc in player.Mercenaries)
            {
                Console.WriteLine("Name: "+merc.Name+"\tHeathPoints: "+merc.CurrentHealthPoints +'/'+ merc.MaxHealthPoints +
                    "\nStrength: "+ merc.Strength +"\tDexterity: "+merc.Dexterity+"\tIntellegence: "+merc.Intellegence+
                    "\nAttack: "+merc.Attack+"\tDefence: "+merc.Defence+"\tExp: "+merc.CurrentExp+'/'+merc.NextLevelExp + '\n');
            }
            Prompt("Hit Enter to Leave.");
        }

        private static Mercenary FireMerc(Player player)
        {
            Console.Clear();
            for(int i = 0; i < player.Mercenaries.Count; i++)
            {
                Console.WriteLine((i + 1) + ".\tName: " + player.Mercenaries[i].Name + "\n\tProfession: " + player.Mercenaries[i].Profession + "\n\tLevel: " + player.Mercenaries[i].Level);
            }
            int choice = BasicMenu("Please Select a Merc To Fire.\nOr Enter A Number Up to 3 Higher then Shown to Cancel.\n", player.Mercenaries.Count + 3);
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
                        Merc.Attack = logic.CalculateAttack(Merc);
                        Merc.Defence = logic.CalculateDefence(Merc);
                        Merc.MaxHealthPoints = logic.CalculateMaxHealthPoints(Merc);
                        Merc.CurrentHealthPoints = Merc.MaxHealthPoints;
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
                        Merc.Attack = logic.CalculateAttack(Merc);
                        Merc.Defence = logic.CalculateDefence(Merc);
                        Merc.MaxHealthPoints = logic.CalculateMaxHealthPoints(Merc);
                        Merc.CurrentHealthPoints = Merc.MaxHealthPoints;
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
                        Merc.Attack = logic.CalculateAttack(Merc);
                        Merc.Defence = logic.CalculateDefence(Merc);
                        Merc.MaxHealthPoints = logic.CalculateMaxHealthPoints(Merc);
                        Merc.CurrentHealthPoints = Merc.MaxHealthPoints;
                        break;
                }
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

        private static Player SellMenu(Player player)
        {
            throw new NotImplementedException();
        }

        private static Player BuyMenu(Player player)
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
