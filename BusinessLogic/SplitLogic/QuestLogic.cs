using DataAccess;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.SplitLogic
{
    public class QuestLogic
    {
        public List<Quest> GetAllQuests()
        {
            throw new NotImplementedException();
        }

        public Monster GetMonster(string monsterName)
        {
            return Data.QuestData.GetMonster(monsterName);
        }

    }
}
