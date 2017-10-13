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
                int choice = Menu("Welcome To RPGame C# Edition.\nPlease Make a Selection Below.\n\t1. New Game.\n\t2. Load Game.\n\t3. Quit Game.\n", 3);
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
            Console.Write("Welcome to Character Creation.\n\nWhat is Your Name?\n");
            character.Name = Console.ReadLine();
            int choice = Menu("Please Select a Class From Below.\n1. Warrior.\n2. Rogue.\n3. Wizard.\n", 3);
            if (choice == 1)
            {
                character.CharacterClass = "Warrior";
                character.Strength = 8;
                character.Dexterity = 5;
                character.Constitution = 6;
                character.Intellegence = 3;
                character.ArmourID = "Ragged Clothes";
                character.MainHandID = "Wooden Sword";
                character.OffHandID = "Pot Lid";
            }
            else if (choice == 2)
            {
                character.CharacterClass = "Rogue";
                character.Strength = 5;
                character.Dexterity = 9;
                character.Constitution = 6;
                character.Intellegence = 5;
                character.ArmourID = "Ragged Clothes";
                character.MainHandID = "Wooden Dagger";
                character.OffHandID = "Wooden Dagger";
            }
            else if (choice == 3)
            {
                character.CharacterClass = "Wizard";
                character.Strength = 4;
                character.Dexterity = 4;
                character.Constitution = 5;
                character.Intellegence = 12;
                character.ArmourID = "Ragged Clothes";
                character.MainHandID = "Wooden Sword";
                character.OffHandID = "Pot Lid";
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
                int choice = Menu("Current Health: " + character.CurrentHealthPoints + "\n\nWhat Would You Like to Do?\n\t1. Save.\n\t2. Hunt.\n\t3. Rest.\n\t4. Shop.\n\t5. Quit.\n", 5);
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
                        Console.Write("You feel Refeshed After a Night at the Inn.\n");
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
                int choice = Menu("What Would You Like To Hunt?\n\t1. Forage For Stuff.\n\t2. Fight Monsters.\n\t3. Back.\n", 3);
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
            int RNGods = logic.RNG();
            if (RNGods <= 10)
            {
                //Teasures beyond belief(?)
                return character;
            }
            else if (RNGods > 10 && RNGods <= 25)
            {
                return character;
            }
            else if (RNGods > 25 && RNGods <= 40)
            {
                return character;
            }
            else if (RNGods > 40 && RNGods <= 55)
            {
                Console.Write("You found nothing of use.");
                return character;
            }
            else if (RNGods > 55 && RNGods <= 70)
            {
                return character;
            }
            else if (RNGods > 70 && RNGods <= 85)
            {
                return character;
            }
            else if (RNGods > 85 && RNGods <= 100)
            {
                //Death or very close to it
                return character;
            }
            else { Console.Write("An Error Occured. Returning to Hunting Screen.\n"); return c; }
        }

        static Character HuntMonsters(Character c)
        {
            bool isHuntingMonsters = true;
            do
            {
                int choice = Menu("Which Monster do you plan to hunt and possible kill?\n\t1. Goblin.\n\t2. Hobgoblin.\n\t3. Ogre.\n\t4. Nevermind.\n", 4);
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
            Monster mon = logic.GetMonster(monsterName);
            Character character = c;
            bool flee = false;
            do
            {
                bool AIAction = logic.AIAction();
                int choice = Menu("Your Health: " + character.CurrentHealthPoints + "\nMonsters Health: " +
                    mon.CurrentHealthPoints + "\nWhat will you do?\n\t1. Attack.\n\t2. Defend. \n\t3. Flee.\n", 3);
                switch (choice)
                {
                    case (1):
                        mon.CurrentHealthPoints -= logic.Damage(c.Attack, mon.Defence, AIAction, logic.Crit());
                        character.CurrentHealthPoints -= logic.Damage(mon.Attack, character.Defence, false, logic.Crit());
                        break;
                    case (2):
                        mon.CurrentHealthPoints -= logic.Damage(c.Attack, mon.Defence, AIAction, logic.Crit());
                        character.CurrentHealthPoints -= logic.Damage(mon.Attack, character.Defence, true, logic.Crit());
                        break;
                    case (3):
                        flee = logic.Flee();
                        break;
                }
            } while ((character.CurrentHealthPoints > 0 && mon.CurrentHealthPoints > 0) || flee == true);
            if (character.CurrentHealthPoints > 0)
            {
                return logic.Looting(character, mon);
            }
            else { return null; }
        }

        private static Character Shop(Character c)
        {
            bool isShopping = true;
            do
            {
                int choice = Menu("What Would you like to do in the market?\n\t1. Buy.\n\t2. Sell.\n\t3.Leave.\n", 3);
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
            throw new NotImplementedException();
        }

        private static Character Buy(Character c)
        {
            throw new NotImplementedException();
        }
        static int Menu(string text, int numOfChoices)
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
            } while (!(choice > 0 && choice < (numOfChoices + 1)) || choice == 0);
            return choice;
        }
    }
}
