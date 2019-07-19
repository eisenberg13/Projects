using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// C# object holding all the information for a reservation in the database
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Unique ID number for a reservation, this table's primary key
        /// </summary>
        public int ReservationID { get; set; }

        /// <summary>
        /// The ID number of the site where this reservation will stay, this table's foreign key
        /// </summary>
        public int SiteID { get; set; }

        /// <summary>
        /// The name of the party who reserved this site
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date the campers are set to arrive
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// The date the campers are set to depart
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// The date the reservation was made
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}