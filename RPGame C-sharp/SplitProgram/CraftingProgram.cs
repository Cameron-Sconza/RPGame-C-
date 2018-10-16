using Library.Models;
using System;

namespace RPGame_C_sharp.SplitProgram
{
    class CraftingProgram
    {
        public Player CraftingMenu(Player p)
        {
            bool isCrafting = true;
            Player player = p;
            do
            {
                Console.Clear();
                int choice = Program.BasicMenu("What Would You Like to Do?\n\t1. Craft Weapons.\n\t2. Craft Armour.\n\t3. Craft Potions.\n\t4. Craft Misc.\n\t5. Back.\n", 5);
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

        public Player CraftMisc(Player player)
        {
            throw new NotImplementedException();
        }

        public Player CraftPotion(Player player)
        {
            throw new NotImplementedException();
        }

        public Player CraftArmour(Player player)
        {
            throw new NotImplementedException();
        }

        public Player CraftWeapon(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
