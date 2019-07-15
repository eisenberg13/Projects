using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.DAL
{
    public interface IRecipeDAO
    {
        int CreateRecipe(Recipe recipe);
        IList<Recipe> GetAllRecipes();
        Recipe GetRecipe(int Id);
       
    }
}
