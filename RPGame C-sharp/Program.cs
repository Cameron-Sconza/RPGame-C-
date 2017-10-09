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
                int choice = Menu("Welcome To RPGame C# Edition.\nPlease Make a Selection Below.\n\t1. New Game.\n\t2. Load Game.\n\t3. Quit Game.\n");
                Thread.Sleep(1000);
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
                    default:
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
            Console.Write("Welcome to Character Creation.\nWhat is Your Name Going to Be?\n");
            character.Name = Console.ReadLine();
            int choice = Menu("Please Select a Class From Below.\n1. Warrior.\n2.Rogue.\n3. Wizard.\n");
            Thread.Sleep(1000);
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
            do
            {
                Console.Clear();
                Character c = character;
                int choice = Menu("Current Health: " + character.CurrentHealthPoints + "\n\nWhat Would You Like to Do?\n\t1. Save Game.\n\t2. Hunt.\n\t3. Rest.\n\t4. Quit.");
                Thread.Sleep(1000);
                switch (choice)
                {
                    case (1):
                        logic.SaveGame(c);
                        break;
                    case (2):
                        c = Hunt(c);
                        break;
                    case (3):
                        c.CurrentHealthPoints = c.MaxHealthPoints;
                        Console.Write("You feel Refeshed After a Night at the Inn.\n");
                        Thread.Sleep(1000);
                        break;
                    case (4):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
        }

        static Character Hunt(Character c)
        {
            throw new NotImplementedException();
        }

        static int Menu(string text)
        {
            int choice;
            int fail = 0;
            do
            {
                Console.Clear();
                Console.Write(text);
                if (fail > 3)
                {
                    Console.WriteLine("Please Try to Just Use the Number. No Enter Key Required.");
                }
                char choiceStr = Console.ReadKey().KeyChar;
                int.TryParse(choiceStr.ToString(), out choice);
                fail++;
            } while (!(choice > 0 && choice < 4) || choice == 0);
            return choice;
        }
    }
}
