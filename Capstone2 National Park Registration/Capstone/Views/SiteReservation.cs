using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Capstone.DAL;

/* This is where the user sees sites that match their search criteria (if they exist)
 * The user selects a site and enters a name
 * Then it goes into the DAO, makes the reservation, and gives the ID number of the reservation
 * If the reservation fails, we announce it to the user here
 */

namespace Capstone.Views
{
    /// <summary>
    /// Menu to make a reservation
    /// </summary>
    public class SiteReservation : CLIMenu
    {
        /// <summary>
        /// List of eligible sites
        /// </summary>
        List<Site> siteList;

        /// <summary>
        /// Beginning of reservation
        /// </summary>
        DateTime start;

        /// <summary>
        /// End of reservation
        /// </summary>
        DateTime end;
       
        // Why did we make a menu that needs eight parameters?
        /// <summary>
        /// Constructor for this menu
        /// </summary>
        /// <param name="parkDAO">DAO for parks table</param>
        /// <param name="campgroundDAO">DAO for reservations table</param>
        /// <param name="siteDAO">DAO for sites table</param>
        /// <param name="reservationDAO">DAO for reservations table</param>
        /// <param name="sites">List of sites that are eligible for the user's reservation</param>
        /// <param name="arrive">Start date of our reservation</param>
        /// <param name="depart">End date of our reservation</param>
        /// <param name="camp">Camp that the user selected</param>
        public SiteReservation(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO, List<Site> sites, DateTime arrive, DateTime depart, Campground camp)
            : base(parkDAO, campgroundDAO, siteDAO, reservationDAO)
        {
            siteList = sites;
            start = arrive;
            end = depart;

            decimal fee;
            fee = campgroundDAO.CostOfSite(camp, arrive, depart);
            this.Title = "*** Results Matching Your Seach Criteria (Make a Selection) ***";
            Labels = $"{"Site No.",12}{"Max Occup.",17}{"Accessible?",21}{"Max RV Length",20}{"Utility",12}{"Cost",12}";
            for(int i = 0; i < siteList.Count; i++)
            {
                this.menuOptions.Add((i+1).ToString(), $"{siteList[i].SiteID,-15}{siteList[i].MaxOccupancy,-20}" +
                    $"{siteList[i].Accessible,-18}{siteList[i].MaxRvLength,-18}{siteList[i].Utilities,-15}{fee,-15:C}");
            }
            this.menuOptions.Add("Q", "Return to previous menu");
        }

        /// <summary>
        /// IF the user still wants to make a reservation, it happens here
        /// </summary>
        /// <param name="choice">String of the user's menu choice</param>
        /// <returns>False in all cases, to return to previous menu</returns>
        protected override bool ExecuteSelection(string choice)
        {
            int resID = 0;
            if (menuOptions.ContainsKey(choice))
            {
                // Make choice into an int, subtract one because of indexing
                int index = int.Parse(choice);
                index--;
                Site site = siteList[index];
                // Get reservation name
                Console.Write("What name should the reservation be made under: ");
                string name = Console.ReadLine();

                // Go to this object to make the SQL insertion
                resID = MyReservationDAO.MakeReservation(site, start, end, name);
            }
            
            // Announce result
            if (resID > 0)
            {
                Console.WriteLine($"Your reservation has been booked and your reservation ID number is {resID}.");
            }
            else
            {
                Console.WriteLine("There was an error, and your reservation was not made.");
            }
            Console.ReadKey();
            return false;
        }
    }
}