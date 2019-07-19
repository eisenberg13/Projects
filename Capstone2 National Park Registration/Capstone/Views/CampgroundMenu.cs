using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

/* This is where we see a list of campgrounds
 * its name, which park it is, when it is open
 * It receives a park object to print its name and acquire its park ID
 * Then it prints the information for each campground in that park
 */

namespace Capstone.Views
{
    /// <summary>
    /// Menu for selecting a campground
    /// </summary>
    public class CampgroundMenu : CLIMenu
    {
        /// <summary>
        /// Park that the user selected earlier
        /// </summary>
        private Park MyPark { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parkDAO">DAO for parks table</param>
        /// <param name="campgroundDAO">DAO for campgrounds table</param>
        /// <param name="siteDAO">DAO for sites table</param>
        /// <param name="reservationDAO">DAO for reservation table</param>
        /// <param name="park">Object for a park that the user selected earlier</param>
        public CampgroundMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO, Park park) :
            base(parkDAO, campgroundDAO, siteDAO, reservationDAO)
        {
            MyPark = park;
            
            this.menuOptions.Add("1", "Search for Available Reservation");
            this.menuOptions.Add("2", "Return to Previous Menu");
        }

        /// <summary>
        /// Handle user menu choice
        /// </summary>
        /// <param name="choice">The user's selection</param>
        /// <returns>True to continue, false to go back to previous menu</returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    ReservationMenu resmenu = new ReservationMenu(MyParksDAO, MyCampgroundDAO, MySiteDAO, MyReservationDAO, MyPark);
                    resmenu.Run();
                    return true;
                case "2":
                    return false;
            }
            return true;
        }

        /// <summary>
        /// What to see before camp selections
        /// </summary>
        protected override void DisplayBeforeMenu()
        {
            Console.WriteLine($"{MyPark.Name} National Campgrounds\r\n");
            Console.WriteLine($"{"Name",10} {"Open",33} {"Close", 16} {"Daily Fee",18}");
            List<Campground> list = MyCampgroundDAO.GetAllCampgrounds(MyPark.ParkID);

            int i = 1;
            foreach(Campground camp in list)
            {
                Console.WriteLine($"#{i, -5}{camp.Name,-30}\t{Month(camp.OpenFrom),-15}\t" +
                    $"{Month(camp.OpenTo),-15}{camp.DailyFee,-15:C}");
                i++;
            }
        }
    }
}