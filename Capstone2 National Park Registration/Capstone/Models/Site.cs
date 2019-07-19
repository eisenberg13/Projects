using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// Object in C# to hold all the information for one campsite in the database
    /// </summary>
    public class Site
    {
        /// <summary>
        /// Unique ID number for the campsite, Primary key in the database
        /// </summary>
        public int SiteID { get; set; }

        /// <summary>
        /// ID number of the campground where the site is located, a foreign key
        /// </summary>
        public int CampgroundID { get; set; }

        /// <summary>
        /// Number for the site within the campground, arbitrarily assigned, not a key
        /// </summary>
        public int SiteNumber { get; set; }

        /// <summary>
        /// Max amount of campers for this site
        /// </summary>
        public int MaxOccupancy { get; set; }

        /// <summary>
        /// Whether this campsite is handicap accessible
        /// </summary>
        public bool Accessible { get; set; }

        /// <summary>
        /// Max length for an RV at this site, 0 for sites that don't allow RVs
        /// </summary>
        public int MaxRvLength { get; set; }

        /// <summary>
        /// Whether this site has utilities like running water and electricity
        /// </summary>
        public bool Utilities { get; set; }
    }
}