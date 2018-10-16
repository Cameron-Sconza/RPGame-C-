using System;
using Library.Models;
using BusinessLogic;
using System.Threading;
using RPGame_C_sharp.SplitProgram;

namespace RPGame_C_sharp
{
    class Program
    {
        public static CraftingProgram CraftingProgram { get; set; }
        public static MercenaryProgram MercenaryProgram { get; set; }
        public static QuestingProgram QuestingProgram { get; set; }
        public static ShopProgram ShopProgram { get; set; }

        public Program()
        {
            CraftingProgram = new CraftingProgram();
            MercenaryProgram = new MercenaryProgram();
            QuestingProgram = new QuestingProgram();
            ShopProgram = new ShopProgram();
        }

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

        static void LoadGame()
        {
            Player Player = Logic.PlayerLogic.LoadGame();
            if (Player != Player.Empty)
            {
                PlayGame(Player);
            }
            else { Console.WriteLine("No Saved Data.\nReturning to Main Menu.\nPlease Wait one Moment..."); Thread.Sleep(2000); }
        }

        static void NewGame()
        {
            Player player = new Player
            {
                Name = Prompt("What Is Your Name?"),
                Gold = 500
            };
            Console.WriteLine("You Will Be Redirected to Hire Your First Mercenary Shortly.\nWithout Any Mercenaries, You Will Not Be Able to Do Much.");
            Thread.Sleep(1000);
            player.Mercenaries.Add(MercenaryProgram.HireMerc(player));
            PlayGame(player);
        }

        static void PlayGame(Player p)
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
                        player = MercenaryProgram.MercMenu(player);
                        break;
                    case (3):
                        player = CraftingProgram.CraftingMenu(player);
                        break;
                    case (4):
                        player = ShopProgram.ShopMenu(player);
                        break;
                    case (5):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
        }

        public static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public static int BasicMenu(string text, int numOfChoices)
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
