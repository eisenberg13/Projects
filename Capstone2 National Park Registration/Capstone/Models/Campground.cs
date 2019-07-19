using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// Object to hold all the information for a campground in our database
    /// </summary>
    public class Campground
    {
        /// <summary>
        /// Unique ID number for this campground, and the primary key in the database
        /// </summary>
        public int CampgroundID { get; set; }

        /// <summary>
        /// Foreign key representing the park where the campground is located
        /// </summary>
        public int ParkID { get; set; }

        /// <summary>
        /// Name of the campground
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Month that the park opens, stored as an integer
        /// </summary>
        public int OpenFrom { get; set; }

        /// <summary>
        /// Month that the park closes, stored as an integer
        /// </summary>
        public int OpenTo { get; set; }

        /// <summary>
        /// Daily fee for camping at this campground (same for all sites)
        /// </summary>
        public decimal DailyFee { get; set; }
    }
}