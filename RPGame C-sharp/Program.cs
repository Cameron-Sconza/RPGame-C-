﻿using System;
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

        static void LoadGame()
        {
            Character character = logic.LoadGame();
            if (character != null)
            {
                PlayGame(character);
            }
            else { Console.WriteLine("No Saved Data.\nReturning to Main Menu."); Thread.Sleep(1000); }
        }

        static void NewGame()
        {
            Console.Clear();
            Character character = new Character { Level = 1, CurrentExp = 0, NextLevelExp = 10 };
            Console.Write("Welcome to Character Creation.\n\nWhat is Your Name?\n");
            character.Name = Console.ReadLine();
            int choice = BasicMenu("Please Select a Class From Below.\n1. Warrior.\n2. Rogue.\n3. Wizard.\n", 3);
            if (choice == 1)
            {
                character.CharacterClass = "Warrior";
                character.Strength = 8;
                character.Dexterity = 5;
                character.Constitution = 6;
                character.Intellegence = 3;
            }
            else if (choice == 2)
            {
                character.CharacterClass = "Rogue";
                character.Strength = 5;
                character.Dexterity = 9;
                character.Constitution = 6;
                character.Intellegence = 5;
            }
            else if (choice == 3)
            {
                character.CharacterClass = "Wizard";
                character.Strength = 4;
                character.Dexterity = 4;
                character.Constitution = 5;
                character.Intellegence = 12;
            }
            character.Attack = logic.CalculateAttack(character.Strength, character.Dexterity, character.Intellegence, character.CharacterClass);
            character.Defence = logic.CalculateDefence(character.Strength, character.Dexterity);
            character.MaxHealthPoints = logic.CalculateMaxHealthPoints(character.Level, character.Strength, character.Dexterity, character.Constitution);
            character.CurrentHealthPoints = character.MaxHealthPoints;
            PlayGame(character);
        }

        static void PlayGame(Character character)
        {
            bool isPlaying = true;
            Character c = character;
            do
            {
                Console.Clear();
                int choice = BasicMenu("Current Health: " + character.CurrentHealthPoints + "\n\nWhat Would You Like to Do?\n\t1. Save.\n\t2. Hunt.\n\t3. Rest.\n\t4. Shop.\n\t5. Quit.\n", 5);
                switch (choice)
                {
                    case (1):
                        logic.SaveGame(c);
                        break;
                    case (2):
                        c = Hunt(c);
                        if (c == null)
                        {
                            Console.WriteLine("You Have Died.");
                            Thread.Sleep(3);
                            isPlaying = false;
                        }
                        break;
                    case (3):
                        c.CurrentHealthPoints = c.MaxHealthPoints;
                        Console.Write("\nYou feel Refeshed After a Night at the Inn.\n");
                        Thread.Sleep(1000);
                        break;
                    case (4):
                        c = Shop(c);
                        break;
                    case (5):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
        }

        static Character Hunt(Character c)
        {
            Character character = c;
            bool isHunting = true;
            do
            {
                int choice = BasicMenu("What Would You Like To Hunt?\n\t1. Forage For Stuff.\n\t2. Fight Monsters.\n\t3. Back.\n", 3);
                switch (choice)
                {
                    case (1):
                        character = Forage(character);
                        break;
                    case (2):
                        character = HuntMonsters(character);
                        break;
                    case (3):
                        isHunting = false;
                        break;
                }
                if (character == null) { return null; }
            } while (isHunting);
            return character;
        }

        static Character Forage(Character c)
        {
            Character character = c;
            return c;
        }

        static Character HuntMonsters(Character c)
        {
            bool isHuntingMonsters = true;
            do
            {
                int choice = BasicMenu("Which Monster do you plan to hunt and possible kill?\n\t1. Goblin.\n\t2. Hobgoblin.\n\t3. Ogre.\n\t4. Nevermind.\n", 4);
                switch (choice)
                {
                    case (1):
                        return Fight("Goblin", c);
                    case (2):
                        return Fight("Hobgoblin", c);
                    case (3):
                        return Fight("Ogre", c);
                    case (4):
                        isHuntingMonsters = false;
                        break;
                }
            } while (isHuntingMonsters);
            return c;
        }

        private static Character Fight(string monsterName, Character c)
        {
            return null;
        }

        private static Character Shop(Character c)
        {
            bool isShopping = true;
            do
            {
                int choice = BasicMenu("What Would you like to do in the market?\n\t1. Buy.\n\t2. Sell.\n\t3.Leave.\n", 3);
                switch (choice)
                {
                    case (1):
                        return Buy(c);
                    case (2):
                        return Sell(c);
                    case (3):
                        isShopping = false;
                        break;
                }
            } while (isShopping);
            return c;
        }

        private static Character Sell(Character c)
        {
            Character character = c;
            bool isSellingItems = true;
            do
            {
                Console.WriteLine("Current Gold:" + character.Gold);
                int choice = StoreMenu("Please select an item to Sell.", character.ItemBackpack);
                if (choice > character.ItemBackpack.Count)
                {
                    isSellingItems = false;
                }
                else
                {
                    Item item = logic.GrabItem(character.ItemBackpack[choice-1]);
                    character.Gold += item.SellPrice;
                    character.ItemBackpack.Remove(item);
                }
            } while (isSellingItems);
            return character;
        }

        private static Character Buy(Character c)
        {
            Character character = c;
            List<Item> list = logic.GetShopItemNames();
            bool isBuyingItems = true;
            do
            {
                Console.WriteLine("Current Gold:" + character.Gold);
                int choice = StoreMenu("Please select an item to Buy.\n", list);
                if (choice > list.Count)
                {
                    isBuyingItems = false;
                }
                else
                {
                    Item item = list[choice-1];
                    if ((character.Gold - item.BuyPrice) < 0)
                    {
                        Console.WriteLine("Not Enough Gold.");
                        Thread.Sleep(750);
                    }
                    else
                    {
                        character.Gold -= item.BuyPrice;
                        character.ItemBackpack.Add(item);
                    }
                }
            } while (isBuyingItems);
            return character;
        }

        static int BasicMenu(string text, int numOfChoices)
        {
            int choice;
            int fail = 0;
            do
            {
                Console.Clear();
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

        static int StoreMenu(string text, List<Item> list)
        {
            List<Item> itemList = list;
            int choice;
            int fail = 0;
            do
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ".\t Item: " + itemList[i].Name + ".\n\t Price: " + itemList[i].BuyPrice);
                }
                Console.WriteLine(text + "\nOr Enter a Number Higher than Displayed to Leave.");
                if (fail > 3)
                {
                    Console.WriteLine("Please Use a Numerical Value.\n(Handles up to +20 of displayed.)");
                }
                int.TryParse(Console.ReadLine(), out choice);
                fail++;
            } while (!(choice > 0 && choice < (list.Count + 20)) || choice == 0);
            return choice;
        }

        static int StoreMenu(string text, List<string> list)
        {
            List<string> itemList = list;
            int choice;
            int fail = 0;
            do
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ".\tItem: " + itemList[i]);
                }
                Console.WriteLine(text + "\nOr Enter a Number Higher than Your Inventory Count to Leave.");
                if (fail > 3)
                {
                    Console.WriteLine("Please Use a Numerical Value.\n(Handles up to +20 of displayed.)");
                }
                int.TryParse(Console.ReadLine(), out choice);
                fail++;
            } while (!(choice > 0 && choice < (list.Count + 20)) || choice == 0);
            return choice;
        }
    }
}
