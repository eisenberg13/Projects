using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        /// <summary>
        /// Get all sites for a given campground
        /// </summary>
        /// <param name="campgroundID">The unique ID number of the campground whose sites we want</param>
        /// <returns>A list of Site objects associated with a given campground</returns>
        List<Site> GetAllSites(int campgroundID);

        /// <summary>
        /// Returns a list of all sites available for a given campground and range date
        /// </summary>
        /// <param name="camp">The campground where the user wants to stay</param>
        /// <param name="startDate">The date when the reservation starts</param>
        /// <param name="endDate">The date when the reservation ends</param>
        /// <returns>A list of up to five sites that are eligible for this reservation</returns>
        List<Site> AvailableSites(Campground camp, DateTime startDate, DateTime endDate);
    }
}