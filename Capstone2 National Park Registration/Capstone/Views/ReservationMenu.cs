using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Views;

/* This where the user selects their campsite
 * It will go one more level down to make the reservation using the Reservation DAO
 */

namespace Capstone.Views
{
    /// <summary>
    /// Here the user searches for a campground reservation
    /// </summary>
    public class ReservationMenu : CLIMenu
    {
        /// <summary>
        /// The park where the user chooses a campground
        /// </summary>
        Park MyPark;

        /// <summary>
        /// List of campgrounds made for that park, the user will select one to see which site will be available for their dates
        /// </summary>
        List<Campground> Camps;

        /// <summary>
        /// Constructor, requires DAO and park object
        /// </summary>
        /// <param name="parkDAO">DAO for parks created at startup</param>
        /// <param name="campgroundDAO">DAO for campgrounds created at startup</param>
        /// <param name="siteDAO">DAO for sites created at startup</param>
        /// <param name="reservationDAO">DAO for reservations created at startup</param>
        /// <param name="park">The park the user selected earlier</param>
        public ReservationMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO, Park park) : 
            base(parkDAO, campgroundDAO, siteDAO, reservationDAO)
        {
            MyPark = park;
            this.Title = "*** Search for Campground Reservation ***";
            this.Labels = $"{"Name",8}{"Open",35}{"Close",16}{"Daily Fee",19:C}";

            // Get camps and create a menu option for each one
            Camps = MyCampgroundDAO.GetAllCampgrounds(MyPark.ParkID);

            for(int i = 0; i < Camps.Count; i++)
            {
                this.menuOptions.Add((i+1).ToString(),($"{Camps[i].Name,-35}{Month(Camps[i].OpenFrom),-15}" +
                    $"{Month(Camps[i].OpenTo),-15}{Camps[i].DailyFee,-20:C}"));
            }
            this.menuOptions.Add("Q", "Return to previous menu");
        }

        /// <summary>
        /// The users input choice is handled here
        /// </summary>
        /// <param name="choice">The string of the user's selection</param>
        /// <returns>True except for conditions defined in base class</returns>
        protected override bool ExecuteSelection(string choice)
        {
            int index;
            List<Site> sites = new List<Site>();
            if(menuOptions.ContainsKey(choice))
            {
                index = int.Parse(choice);
                index--;
                Campground camp = Camps[index];

                // Get dates
                Console.Write("What is the arrival date? YYYY/MM/DD: ");
                string input = Console.ReadLine();
                DateTime arriveDate = DateTime.Parse(input);

                Console.Write("What is the departure date? YYYY/MM/DD: ");
                input = Console.ReadLine();
                DateTime departDate = DateTime.Parse(input);

                // Get list of available sites, go to next menu or announce no sites are available
                sites = MySiteDAO.AvailableSites(camp, arriveDate, departDate);
                if (sites.Count == 0 )
                {
                    Console.WriteLine("No available sites for those dates");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    SiteReservation siteres = 
                        new SiteReservation(MyParksDAO, MyCampgroundDAO, MySiteDAO, MyReservationDAO, sites, arriveDate, departDate, camp);
                    siteres.Run();
                }
            }
            return true;
        }
    }
}