using System.Collections.Generic;
using Library.Models;
using DataAccess;

namespace BusinessLogic
{
    public class Logic
    {
        static Data data;

        public Logic(Data D) => data = D;

        public Character LoadGame()
        {
            return data.LoadGame();
        }
    }
}
