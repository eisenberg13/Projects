using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// Object to hold all information for a park in the database
    /// </summary>
    public class Park
    {
        // Properties

        /// <summary>
        /// Unique ID number, and primary key in the dictionary
        /// </summary>
        public int ParkID { get; set; }

        /// <summary>
        /// Name of the park
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Location of the park (state)
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Date of the park's establishment
        /// </summary>
        public DateTime EstablishDate { get; set; }

        /// <summary>
        /// Area of the park in acres
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// Number of visitors per year
        /// </summary>
        public int Visitors { get; set; }

        /// <summary>
        /// Short description of the park
        /// </summary>
        public string Description { get; set; }
    }
}