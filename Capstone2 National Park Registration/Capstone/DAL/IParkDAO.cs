using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IParkDAO
    {
        /// <summary>
        /// Get all parks
        /// </summary>
        /// <returns>A list of all parks in our database</returns>
        List<Park> GetAllParks();
    }
}