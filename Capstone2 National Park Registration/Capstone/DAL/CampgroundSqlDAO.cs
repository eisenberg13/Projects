using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;

/* This object is how we acquire information about our campgrounds
 * Like ParkSqlDAO, it uses a list that can get us all the information we need
 * about all the campgrounds in a given park
 * 
 * It also contains a method to calculate the fee for a stay at that campground
 */

namespace Capstone.DAL
{
    /// <summary>
    /// DAO for access to the campground table
    /// </summary>
    public class CampgroundSqlDAO : ICampgroundDAO
    {
        /// <summary>
        /// Database connection
        /// </summary>
        private string connectionString;

        /// <summary>
        /// CampgroundSqlDAO default constructor
        /// </summary>
        /// <param name="databaseConnectionString">Path to connect to our database</param>
        public CampgroundSqlDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        #region Methods
        /// <summary>
        /// Acquire information about campgrounds
        /// </summary>
        /// <param name="parkID">The unique park ID number for the park whose campgrounds we want</param>
        /// <returns>A lit of all campgrounds for a given park</returns>
        public List<Campground> GetAllCampgrounds(int parkID)
        {
            List<Campground> list = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query for campgrounds in a park
                    string sql = "SELECT * FROM campground WHERE park_id = @parkId ORDER BY name ASC";

                    // Execute query, with string replace to prevent SQL injection
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkId", parkID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Build campgrounds and populate our list
                    while (reader.Read())
                    {
                        Campground obj = new Campground();

                        obj.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        obj.ParkID = Convert.ToInt32(reader["park_id"]);
                        obj.Name = Convert.ToString(reader["name"]);
                        obj.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                        obj.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                        obj.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

                        list.Add(obj);
                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine($"Error in GetAllCampgrounds: {ex.Message}");
            }

            return list;
        }

        /// <summary>
        /// Total price of your stay for a given reservation
        /// </summary>
        /// <param name="camp">The camp where the reservation will be</param>
        /// <param name="arrival">Start date of reservation</param>
        /// <param name="departure">End date of reservation</param>
        /// <returns>Decimal representing the total fee for that reservation</returns>
        public decimal CostOfSite(Campground camp, DateTime arrival, DateTime departure)
        {
            decimal fee = 0;

            // Use this function to get total number of days (rather than another date)
            int days = (int)((departure - arrival).TotalDays);
            fee = days * camp.DailyFee;

            return fee;
        }
        #endregion
    }
}