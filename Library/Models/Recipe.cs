using System.Collections.Generic;

namespace Library.Models
{
    public class Recipe
    {
        public string RecipeName { get; set; }
        public long CraftingTime { get; set; }
        public int Difficulty { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
