using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Models
{

    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public int CookTime { get; set; }
        public int PrepTime { get; set; }
        public string Type { get; set; }

        public List<Ingredients> Ingredient { get; set; }

        public Recipe()
        {
            this.Ingredient = new List<Ingredients>();
        }
    }   
}
