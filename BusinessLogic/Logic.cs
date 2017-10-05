using System.Collections.Generic;
using Library.Models;
using DataAccess;
using System;
using System.Linq;

namespace BusinessLogic
{
    public class Logic
    {
        static Data data;
        static Random random;

        public Logic(Data D) => data = D;

        public Character LoadGame()
        {
            Character character = data.LoadGame();
            if (character.CharacterClass == "Rogue")
            {
                character.Attack = Math.Ceiling(a: (((character.Dexterity * 2) / 3) + (character.Strength * 0.5) + 2));
            }
            else if (character.CharacterClass == "Warrior")
            {
                character.Attack = Math.Ceiling(a: (((character.Strength * 2) / 3) + (character.Dexterity * 0.5) + 3));
            }
            else if (character.CharacterClass == "Wizard")
            {
                character.Attack = Math.Ceiling(a: (((character.Intellegence * 2) / 3) + 2));
            }
            character.Defence = Math.Ceiling(a: (((character.Strength + character.Dexterity) / 1.5) + 2));
            character.MaxHealthPoints = Math.Ceiling(a: ((((character.Level * character.Strength + (character.Dexterity * character.Level) / 3)) / 2) + 20));
            character.CurrentHealthPoints = character.MaxHealthPoints;
            return character;
        }

        public void SaveGame(Character character)
        {
            data.SaveGame(character);
        }

        public double Damage(double dealerAttack, double recieverDefence, bool recieverIsDefending, bool crit)
        {
            if (recieverIsDefending)
            {
                recieverDefence *= 2.00;
            }
            if (crit)
            {
                dealerAttack *= 2.50;
            }
            if (dealerAttack > recieverDefence)
            {
                return Math.Ceiling(dealerAttack);
            }
            else if (dealerAttack < recieverDefence)
            {
                return Math.Ceiling(dealerAttack / 2.00);
            }
            else
            {
                return Math.Ceiling(dealerAttack / 1.15);
            }
        }

        public Character Looting(Character character, Monster monster)
        {
            data.GetMonsterLoot().Where(ml => ml.MonsterID == monster.Name);
            return null;
        }
    }
}
