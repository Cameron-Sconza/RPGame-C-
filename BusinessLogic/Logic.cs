using BusinessLogic.SplitLogic;

namespace BusinessLogic
{
    public class Logic
    {
        public static CombatLogic CombatLogic { get; set; }
        public static ItemLogic ItemLogic { get; set; }
        public static PlayerLogic PlayerLogic { get; set; }
        public static QuestLogic QuestLogic { get; set; }
        public static RecipeLogic RecipeLogic { get; set; }
        public static StatsLogic StatsLogic { get; set; }

        public Logic()
        {
            CombatLogic = new CombatLogic();
            ItemLogic = new ItemLogic();
            PlayerLogic = new PlayerLogic();
            QuestLogic = new QuestLogic();
            RecipeLogic = new RecipeLogic();
            StatsLogic = new StatsLogic();
        }
    }
}
