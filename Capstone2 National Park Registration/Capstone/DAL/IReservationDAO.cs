using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
   public interface IReservationDAO
    {
        /// <summary>
        /// This method makes the reservation, after checking for availability
        /// </summary>
        /// <param name="site">The campsite where the user wants to stay</param>
        /// <param name="arrive">The date the reservation starts</param>
        /// <param name="depart">The date the reservation ends</param>
        /// <param name="name">The name the reservation is made under</param>
        /// <returns>A unique ID for the reservation, or 0 if the reservation fails</returns>
        int MakeReservation(Site site, DateTime arrive, DateTime depart, string name);
    }
}