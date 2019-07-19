using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        /* This class will make a query to grab all the park info from the database
         * Then it will construct Park objects from the info in our query
         * It will return that list to the CLI menu
         * 
         * The menu needs to:
         * List all parks by name alphabetically
         * Provide further information about a single park
         * 
         * It can do both of these things using the information that the list provides
         */

         
        /// <summary>
        /// String containing address of our database
        /// </summary>
        private string connectionString;
        
        /// <summary>
        /// Constructor for the ParlSqlDAO class
        /// </summary>
        /// <param name="databaseConnectionString">Database connection string, passed in automatically in program.cs</param>
        public ParkSqlDAO(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
        }
        
        /// <summary>
        /// Queries the database for all parks
        /// </summary>
        /// <returns>A list of all parks</returns>
        public List<Park> GetAllParks()
        {
            // This list starts empty. If the exception handler triggers, we will eventually return an empty list
            List<Park> list = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM park ORDER BY name ASC";
                    
                    // Send query
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
   
                    // Read from query results
                    // Build each park object and add it to our list
                    while (reader.Read())
                    {
                        Park obj = new Park();

                        obj.ParkID = Convert.ToInt32(reader["park_id"]);
                        obj.Name = Convert.ToString(reader["name"]);
                        obj.Location = Convert.ToString(reader["location"]);
                        obj.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        obj.Area = Convert.ToInt32(reader["area"]);
                        obj.Visitors = Convert.ToInt32(reader["visitors"]);
                        obj.Description = Convert.ToString(reader["description"]);

                        list.Add(obj);
                    }
                }
            }

            catch(SqlException ex)
            {
                Console.WriteLine($"Error in GetAllParks: {ex.Message}");
            }

            // No 'finally' statement, it cannot contain a return statement
            return list;
        }
    }
}