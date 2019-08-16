using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class PlantSqlDAO : IPlantDAO
    {
        private readonly string connectionString;

        public PlantSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Plant> GetPlantsByExposure(string exposure)
        {
            try
            {
                List<Plant> plants = new List<Plant>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM plant WHERE exposureToSun like @exposure;", conn);
                    cmd.Parameters.AddWithValue("@exposure", "%" + exposure + "%");

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        plants.Add(MapRowToPlant(reader));
                    }
                }

                return plants;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Plant> GetPlantsFullShade()
        {
            try
            {
                List<Plant> plants = new List<Plant>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM plant WHERE exposureToSun like '%Full Shade%';", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        plants.Add(MapRowToPlant(reader));
                    }
                }

                return plants;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Plant> GetPlantsPartialSun()
        {
            try
            {
                List<Plant> plants = new List<Plant>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM plant WHERE exposureToSun like '%Partial Sun%';", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        plants.Add(MapRowToPlant(reader));
                    }
                }

                return plants;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Plant> GetPlantsFullSun()
        {
            try
            {
                List<Plant> plants = new List<Plant>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM plant WHERE exposureToSun like '%Full Sun%';", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        plants.Add(MapRowToPlant(reader));
                    }
                }

                return plants;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Plant> GetAllPlants()
        {
            try
            {
                List<Plant> plants = new List<Plant>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM plant", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        plants.Add(MapRowToPlant(reader));
                    }
                }

                return plants;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private Plant MapRowToPlant(SqlDataReader reader)
        {
            return new Plant()
            {
                Id = Convert.ToInt32(reader["id"]),
                PlantName = Convert.ToString(reader["name"]),
                Description = Convert.ToString(reader["description"]),
                PlantExposure = Convert.ToString(reader["exposureToSun"]),
                Spread = Convert.ToInt32(reader["spread"]),
                TypeSoldIn = Convert.ToString(reader["typeSold"]),
                Quantity = Convert.ToInt32(reader["quantity"]),
                Price = Convert.ToDecimal(reader["price"]),
                Image = Convert.ToString(reader["img"]),
                Color = Convert.ToString(reader["color"])
            };
        }
    }
}
