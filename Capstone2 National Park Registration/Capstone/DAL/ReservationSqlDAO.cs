using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

/* This is our reservation DAO
 * Unlike the other SQL DAO classes, this class does not perform any query
 * Instead, it inserts a reservation into our database
 */

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {
        /// <summary>
        /// Holds our connection information
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Standard constructor, requires connection
        /// </summary>
        /// <param name="databaseConnectionString">String containing the information needed for our connection</param>
        public ReservationSqlDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }

        /// <summary>
        /// This is the method that makes a SQL insertion to make our reservation, after we've checked it's allowed
        /// </summary>
        /// <param name="site">The campsite where the user wants to stay</param>
        /// <param name="arrive">Start date of the reservation</param>
        /// <param name="depart">End date of the reservation</param>
        /// <param name="name">Name of the reservation holder</param>
        /// <returns>ID number of the reservation, returns 0 if the reservation isn't made</returns>
        public int MakeReservation(Site site, DateTime arrive, DateTime depart, string name)
        {
            // The ID number of our reservation, set to 0 by default
            // If the reservation succeeds, it will be set to a non-0 number
            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL insertion
                    string sql = "Insert Into reservation (site_id, name, from_date, to_date) " +
                        "Values (@siteId, @name, @fromDate, @toDate); Select @@identity";

                    // Create command object and replace strings
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@siteId", site.SiteID);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fromDate", arrive);
                    cmd.Parameters.AddWithValue("@toDate", depart);

                    // Perform insert and get back our value
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine($"Error in MakeReservation: {ex.Message}");
                Console.ReadKey();
                return 0;
            }

            return id;
        }
    }
}