using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Recipes.Models;

namespace Recipes.DAL
{
    public class RecipeSQLDAO : IRecipeDAO
    {
        private readonly string connectionString;

        public RecipeSQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int CreateRecipe(Recipe recipe)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //TODO: Finish this query
                    conn.Open();

                    // Start a transaction
                    SqlTransaction transaction = conn.BeginTransaction();
                    
                    SqlCommand cmd = new SqlCommand(
                       @"INSERT INTO Recipe (name, description, steps, prepTime, cookTime, type) 
                            VALUES (@name, @description, @steps, @prepTime, @cookTime, @type); Select @@Identity;", conn);
                    cmd.Parameters.AddWithValue("@name", recipe.Name);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@steps", recipe.Steps);             
                    cmd.Parameters.AddWithValue("@prepTime", recipe.PrepTime);
                    cmd.Parameters.AddWithValue("@cookTime", recipe.CookTime);
                    cmd.Parameters.AddWithValue("@type", recipe.Type);
                   
                    int recipeId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Loop through each Ingredient and write it also
                    foreach (Ingredients ing in recipe.Ingredient)
                    {
                        SqlCommand cmdIng = new SqlCommand(
                            @"Insert Into Ingredient (name, quantity, unit, recipeId) 
                            VALUES(@name, @quantity, @units, @recipeId);", conn);

                        cmdIng.Parameters.AddWithValue("@Name", ing.Name);
                        cmdIng.Parameters.AddWithValue("@Quantity", ing.Quantity);
                        cmdIng.Parameters.AddWithValue("@Unit", ing.Unit);
                        cmdIng.Parameters.AddWithValue("@RecipeId", recipeId);
                  
                        cmdIng.ExecuteNonQuery();
                    }
                    // commit the transaction
                    transaction.Commit();
                    return recipeId;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        
    }
       
        public IList<Recipe> GetAllRecipes()
        {
            List<Recipe> list = new List<Recipe>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Start a transaction
                    
                    string sql = "Select * from recipe;";
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(MapRowToRecipe(reader));
                    }
                    return list;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Recipe GetRecipe(int Id)
        {
            throw new NotImplementedException();
        }
        private Recipe MapRowToRecipe(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.Id = Convert.ToInt32(reader["Id"]);
            recipe.Name = Convert.ToString(reader["name"]);
            recipe.Description = Convert.ToString(reader["description"]);
            recipe.Steps = Convert.ToString(reader["steps"]);
            recipe.PrepTime = Convert.ToInt32(reader["prepTime"]);
            recipe.CookTime = Convert.ToInt32(reader["cookTime"]);
            recipe.Type = Convert.ToString(reader["type"]);
            return recipe;
        }
        private Ingredients MapRowToIngedient(SqlDataReader reader)
        {
            Ingredients ingredients = new Ingredients();
            ingredients.Name = Convert.ToString(reader["name"]);
            ingredients.Quantity = Convert.ToInt32(reader["quantity"]);
            ingredients.Unit = Convert.ToString(reader["unit"]);
            ingredients.RecipeId = Convert.ToInt32(reader["recipeId"]);
            return ingredients;
        }
    }
}
