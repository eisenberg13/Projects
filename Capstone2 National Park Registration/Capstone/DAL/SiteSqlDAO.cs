using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;

/* This is the class we will use to access information about our sites
 * 
 * One method uses a SQL query to find all sites for a given campground
 * 
 * Another method contains a query that we use when making a reservation
 * It gives us all sites in a campground, that are not already booked for a given date range
 */

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        /// <summary>
        /// Holds our database connection information
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Constructor for SiteSqlDao, creates database connection string
        /// </summary>
        /// <param name="databaseConnectionString">String containing information needed to access our database</param>
        public SiteSqlDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        #region Methods

        /// <summary>
        /// Gets all campsites for a given campground
        /// </summary>
        /// <param name="campgroundID">Unique ID number for the campground in question</param>
        /// <returns>A list of all sites at a given campground</returns>
        public List<Site> GetAllSites(int campgroundID)
        {
            List<Site> list = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query for all sites in a campground
                    string sql = "SELECT * FROM site WHERE campground_id = @campgroundId";

                    // Execute query
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Create site object, fill in information, and populate our list
                    while (reader.Read())
                    {
                        Site obj = new Site();

                        obj.SiteID = Convert.ToInt32(reader["site_id"]);
                        obj.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        obj.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        obj.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        obj.Accessible = Convert.ToBoolean(reader["accessible"]);
                        obj.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        obj.Utilities = Convert.ToBoolean(reader["utilities"]);

                        list.Add(obj);
                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine($"Error in GetAllSites: {ex.Message}");
                throw;
            }

            return list;
        }
        
        /// <summary>
        /// Called while making reservation, returns list of sites that can be booked
        /// </summary>
        /// <param name="camp">The campground we want for our reservation</param>
        /// <param name="startDate">The beginning of the reservation</param>
        /// <param name="endDate">The end of the reservation</param>
        /// <returns>A list of up to 5 campsites that are available for the dates requested</returns>
        public List<Site> AvailableSites(Campground camp, DateTime startDate, DateTime endDate)
        {
            // Start with our empty list
            List<Site> sites = new List<Site>();

            // First check to see that the park will be open for these months
            // If not, we will return an empty list
            if (camp.OpenFrom <= startDate.Month && camp.OpenTo >= endDate.Month)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        /* We want a list of no more than five sites
                         * We need to check the following:
                         * 
                         * The start date or end date of our reservation is not contained within an existing reservation for that site
                         * Also that the old dates are not contained within the new reservation
                         * 
                         * We have to search for all sites that have this, and return everything NOT fulfilling that condition
                         * This requires a join and a subquery
                         */
                        string sql = "SELECT TOP 5 * FROM site " +
                            "JOIN campground ON site.campground_id = campground.campground_id " +
                            "WHERE campground.campground_id = @campID " +
                            "AND site_id NOT IN " +
                            "(SELECT site_id FROM reservation WHERE from_date BETWEEN @fromDate AND @toDate " +
                            "OR to_date BETWEEN @fromDate AND @toDate " +
                            "OR (from_date BETWEEN @fromDate AND @toDate AND to_date Between @fromDate AND @toDate) " +
                            "OR from_date < @fromDate AND to_date > @toDate)";

                        // Create and execute SQL query
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@campID", camp.CampgroundID);
                        cmd.Parameters.AddWithValue("@fromDate", startDate);
                        cmd.Parameters.AddWithValue("@toDate", endDate);

                        SqlDataReader reader = cmd.ExecuteReader();

                        // Create a new list and populate it with our sites
                        while (reader.Read())
                        {
                            Site obj = new Site();

                            obj.SiteID = Convert.ToInt32(reader["site_id"]);
                            obj.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                            obj.SiteNumber = Convert.ToInt32(reader["site_number"]);
                            obj.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                            obj.Accessible = Convert.ToBoolean(reader["accessible"]);
                            obj.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                            obj.Utilities = Convert.ToBoolean(reader["utilities"]);

                            sites.Add(obj);
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine($"Error in AvailableSites: {ex.Message}");
                    Console.ReadKey();
                    return sites;
                }
                
            }// End if
            return sites;
        }
        #endregion
    }
}