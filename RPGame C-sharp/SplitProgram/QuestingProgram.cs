using BusinessLogic;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame_C_sharp.SplitProgram
{
    class QuestingProgram
    {
        public   Player Quest(Player p)
        {
            Player player = p;
            bool isPlaying = true;
            do
            {
                int choice = Program.BasicMenu("Select a Tier.\n\t1. Easy (Reccomended Level 1).\n\t2. Medium (Reccomended Level 10)\n\t3.  Hard (Reccomended Level 25)\n\t4. Leave.\n", 4);
                switch (choice)
                {
                    case (1):
                    case (2):
                    case (3):
                        player = Tier(player, choice);
                        break;
                    case (4):
                        isPlaying = false;
                        break;
                }
            } while (isPlaying);
            return player;
        }

        public   Player Tier(Player p, int tier)
        {
            List<Quest> list = Logic.QuestLogic.GetAllQuests().Where(q => q.Tier == tier).ToList();
            int count = list.Count;
            Program.MercenaryProgram.ViewMerc(p);
            int choice = Program.BasicMenu("Which Quest Would You Like to Sent A Mercenary to Complete?\nOr Enter a Number Up to 3 Higher Then Shown to Leave.", count + 3);
            try
            {
                Questing questing = Questing.Convert(list[choice - 1]);
                questing.Mercenary = Program.MercenaryProgram.SelectMerc(p, "Please Select a Merc To Send On This Quest.");
                if (questing.Mercenary == Mercenary.Empty)
                {
                    return p;
                }
                else
                {
                    return Logic.CombatLogic.Questing(p, questing);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return p;
            }
        }

    }
}
