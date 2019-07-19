using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface ICampgroundDAO
    {
        /// <summary>
        /// Acquire information about campgrounds
        /// </summary>
        /// <param name="parkID">The unique park ID number for the park whose campgrounds we want</param>
        /// <returns>A lit of all campgrounds for a given park</returns>
        List<Campground> GetAllCampgrounds(int parkID);

        /// <summary>
        /// Gets all campgrounds for a given park
        /// </summary>
        /// <param name="parkID">The unique ID number of the park whose campgrounds we want to see</param>
        /// <returns>A list of all campgrounds in a park</returns>
        decimal CostOfSite(Campground camp, DateTime arrival, DateTime departure);
    }
}