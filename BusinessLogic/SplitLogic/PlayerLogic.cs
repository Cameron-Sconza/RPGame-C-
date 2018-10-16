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
            string Test = Data.TestPath;
            Player Player = Data.PlayerData.LoadGame();
            if (Player == null) { return Player.Empty; }
            else { return Player; }
        }

    }
}
