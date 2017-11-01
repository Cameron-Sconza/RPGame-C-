using DataAccess;
using Library.Models;

namespace BusinessLogic.SplitLogic
{
    public class PlayerLogic
    {
        public void SaveGame(Player Player)
        {
            Data.PlayerData.SaveGame(Player);
        }

        public Player LoadGame()
        {
            Player Player = Data.PlayerData.LoadGame();
            if (Player == null) { return null; }
            else { return Player; }
        }

    }
}
