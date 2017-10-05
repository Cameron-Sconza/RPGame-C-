using System;
using System.Collections.Generic;
using Library.Models;
using BusinessLogic;
using System.Threading;

namespace RPGame_C_sharp
{
    class Program
    {
        private static Logic logic;

        public Program(Logic L) => logic = L;

        static void Main(string[] args)
        {
            bool appIsRunning = true;
            do
            {
                int choice;
                int fail = 0;
                do
                {
                    Console.Clear();
                    Console.Write("Welcome To RPGame C# Edition.\nPlease Make a Selection Below.\n\t1. New Game.\n\t2. Load Game.\n\t3. Quit Game.\n");
                    if (fail > 3)
                    {
                        Console.WriteLine("Please Try to just Use the Number. No Enter Key Required.");
                    }
                    char choiceStr = Console.ReadKey().KeyChar;
                    fail++;
                    int.TryParse(choiceStr.ToString(), out choice);
                } while (!(choice > 0 && choice < 4) || choice == 0);
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

        private static void LoadGame()
        {
            Character character = logic.LoadGame();
            if (character != null)
            {
                PlayGame(character);
            }
            else { Console.WriteLine("No Saved Data.\nReturning to Main Menu."); Thread.Sleep(1000); }
        }
        
        private static void NewGame()
        {
            Character character = new Character();
            Console.Write("Welcome to Character Creation.\nWhat is Your Name Going to Be?\n");
            character.Name = Console.ReadLine();

        }

        private static void PlayGame(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
