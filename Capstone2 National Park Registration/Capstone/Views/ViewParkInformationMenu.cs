using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

/* This menu shows the information of the park that the user selected in ViewParksMenu
 * It then asks if they want to see the campgrounds at that park, or make a reservation
 * Finally it directs them to the next menu that they want to see
 */

namespace Capstone.Views
{
    /// <summary>
    /// Show information for a park after the user selects one, and present further options
    /// </summary>
    public class ViewParkInformationMenu : CLIMenu
    {
        /// <summary>
        /// Park that the user selected earlier
        /// </summary>
        Park MyPark;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parkDAO">DAO for parks table</param>
        /// <param name="campgroundDAO">DAO for campgrounds table</param>
        /// <param name="siteDAO">DAO for sites table</param>
        /// <param name="reservationDAO">DAO for reservations table</param>
        /// <param name="park">Park that the user selected</param>
        public ViewParkInformationMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, 
            ISiteDAO siteDAO, IReservationDAO reservationDAO, Park park) :
            base(parkDAO, campgroundDAO, siteDAO, reservationDAO)
        {
            MyPark = park;
            this.Title = "*** Select a command ***";
            this.menuOptions.Add("1", "View Campgrounds");
            this.menuOptions.Add("2", "Search for reservation");
            this.menuOptions.Add("3", "Return to previous screen");
            this.menuOptions.Add("Q", "Quit");
        }

        /// <summary>
        /// Handle user's menu selection here
        /// </summary>
        /// <param name="choice">String of user's menu selection</param>
        /// <returns>True to continue, false to return to previous menu</returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch(choice)
            {
                case "1":
                    // Show campgrounds at this park
                    CampgroundMenu campmenu = new CampgroundMenu(MyParksDAO, MyCampgroundDAO, MySiteDAO, MyReservationDAO, MyPark);
                    campmenu.Run();
                    return true;
                case "2":
                    // Search for a reservation (reservation menu not yet implemented)
                    ReservationMenu resmenu = new ReservationMenu(MyParksDAO, MyCampgroundDAO, MySiteDAO, MyReservationDAO, MyPark);
                    resmenu.Run();
                    return true;
                case "3":
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Display information for the park that the user selected
        /// </summary>
        protected override void DisplayBeforeMenu()
        {
            Console.WriteLine($"{MyPark.Name} National Park");
            Console.WriteLine($"{"Location:",-30}{MyPark.Location,-20}");
            Console.WriteLine($"{"Established:",-30}{MyPark.EstablishDate.Date,-20:d}");
            Console.WriteLine($"{"Area:",-30}{MyPark.Area,-6} sq km");
            Console.WriteLine($"{"Annual Visitors:",-30}{MyPark.Visitors,-20}\r\n");
            Console.WriteLine(MyPark.Description);
        }
    }
}