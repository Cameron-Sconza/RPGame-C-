using System.Collections.Generic;

namespace Library.Crafting
{
    public class Recipe
    {
        public string RecipeName { get; set; }
        public int CraftingTime { get; set; }
        public int Difficulty { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {   
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}
