using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipes.DAL;
using Recipes.Models;

namespace Recipes.Controllers
{
    public class HomeController : Controller
    {
        private IRecipeDAO recipeDAO;

        public HomeController(IRecipeDAO recipeDAO)
        {
            this.recipeDAO = recipeDAO;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IList<Recipe> recipes = new List<Recipe>();
            recipes = recipeDAO.GetAllRecipes();
            return View(recipes);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
