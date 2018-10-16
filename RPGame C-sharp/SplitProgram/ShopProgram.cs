using Library.Models;
using System;

namespace RPGame_C_sharp.SplitProgram
{
    class ShopProgram
    {
        public Player ShopMenu(Player p)
        {
            bool isPlaying = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = Program.BasicMenu("What Would You Like to Do?\n\t1. Buy.\n\t2. Sell.\n\t3. Quit.\n", 3);
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

        public Player SellMenu(Player p)
        {
            Player player = p;
            bool isPlaying = true;
            do
            {
                int choice = Program.BasicMenu("What Would You Like to Sell?\n\t1. Items.\n\t2. Equipment.\n\t3. Nevermind.", 3);
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

        public Player SellEquipment(Player player)
        {
            throw new NotImplementedException();
        }

        public Player SellItems(Player player)
        {
            throw new NotImplementedException();
        }

        public Player BuyMenu(Player p)
        {
            Player player = p;
            bool isPlaying = true;
            do
            {
                int choice = Program.BasicMenu("What Would You Like to Sell?\n\t1. Items.\n\t2. Equipment.\n\t3. Nevermind.", 3);
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

        public Player BuyEquipment(Player player)
        {
            throw new NotImplementedException();
        }

        public Player BuyItems(Player player)
        {
            throw new NotImplementedException();
        }

    }
}
