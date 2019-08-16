using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class PlotSqlDAO : IPlotDAO
    {
        private readonly string connectionString;

        public PlotSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int CreatePlot(int userId, Plot plot)
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
                        @"INSERT INTO Plot (name, length, width, exposureToSun, userId) 
                            VALUES (@Name, @Length, @Width, @Exposure, @UserId); Select @@Identity;", conn);
                    cmd.Parameters.AddWithValue("@Name", plot.PlotName);
                    cmd.Parameters.AddWithValue("@Length", plot.Length);
                    cmd.Parameters.AddWithValue("@Width", plot.Width);
                    cmd.Parameters.AddWithValue("@Exposure", plot.PlotExposure);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Transaction = transaction;

                    int plotId = Convert.ToInt32(cmd.ExecuteScalar());

                    // commit the transaction
                    transaction.Commit();
                    return plotId;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public bool AddPlantToPlot(Plot plot, Plant plant)
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
                        @"INSERT INTO plantPlot  (plotId, plantId) 
                            VALUES (@PlotId, @PlantId);", conn);
                    cmd.Parameters.AddWithValue("@PlotId", plot.Id );
                    cmd.Parameters.AddWithValue("@PlantId", plant.Id);               
                    cmd.Transaction = transaction;

                    transaction.Commit();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public Plot GetPlotById(int id)
        {
            Plot plot = new Plot();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from plot where id = @plotId", conn);
                    cmd.Parameters.AddWithValue("@plotId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        plot = MapToRow(reader);
                    }
                    return plot;
                }
            
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Plot> GetUsersPlots(int userId)
        {
            List<Plot> plots = new List<Plot>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from plot where userId = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        plots.Add(MapToRow(reader));
                    }


                    return plots;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private Plot MapToRow(SqlDataReader reader)
        {
            Plot plot = new Plot();
            plot.Id = Convert.ToInt32(reader["id"]);
            plot.PlotName = Convert.ToString(reader["name"]);
            plot.Length = Convert.ToInt32(reader["length"]);
            plot.Width = Convert.ToInt32(reader["width"]);
            plot.PlotExposure = Convert.ToString(reader["exposureToSun"]);
            plot.UserId = Convert.ToInt32(reader["userId"]);
            return plot;
        }

        public bool AddPlantToPlot(int plotId, int plantId)
        {
            throw new NotImplementedException();
        }
    }
}
