using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class PlantPlotSqlDAO : IPlantPlotDAO
    {
        private readonly string connectionString;

        public PlantPlotSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<PlantPlot> GetPlantPlotInfo(int plotId)
        {
            List<PlantPlot> output = new List<PlantPlot>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from plantPlot where plotId = @plotId", conn);

                    cmd.Parameters.AddWithValue("@plotId", plotId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        output.Add(MapToRow(reader));
                    }

                    return output;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private PlantPlot MapToRow(SqlDataReader reader)
        {
            PlantPlot pp = new PlantPlot();
            pp.plantId = Convert.ToInt32(reader["plantId"]);
            pp.top = Convert.ToInt32(reader["locationX"]);
            pp.left = Convert.ToInt32(reader["locationY"]);
            return pp;
        }

        public void SavePlotInfo(PlantPlot[] plantPlot, int plotId)
        {
            try
            {

                //string sql = "begin tran;" +
                //    "delete plantPlot where plotId = @plotId;";
                //foreach(PlantPlot PP in plantPlot)
                //{
                //    sql += "insert into plantPlot "
                //}
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //TODO: Finish this query
                    conn.Open();

                    // Start a transaction
                    SqlTransaction transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("delete plantPlot where plotId = @plotId;", conn);
                    cmd.Parameters.AddWithValue("@plotId", plotId);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                    foreach (PlantPlot PP in plantPlot)
                    {
                        cmd = new SqlCommand(
                         @"INSERT INTO PlantPlot (plotId, plantId, locationX, locationY) 
                                VALUES (@plotId, @plantId, @LocationX, @LocationY);", conn);
                        cmd.Parameters.AddWithValue("@plotId", plotId);
                        cmd.Parameters.AddWithValue("@plantId", PP.plantId);
                        cmd.Parameters.AddWithValue("@LocationX", PP.top);
                        cmd.Parameters.AddWithValue("@LocationY", PP.left);
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                    }

                    // commit the transaction
                    transaction.Commit();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


    }
}

