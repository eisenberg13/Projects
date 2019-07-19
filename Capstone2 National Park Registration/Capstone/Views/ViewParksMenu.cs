using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Views;

/* This is the first menu that runs, called directly from program.cs
 * This is where the user sees a list of parks and selects one of the parks available
 * Then it goes to ViewParkInformationMenu
 */

namespace Capstone.Views
{
    /// <summary>
    /// The top level menu where the user selects a park
    /// </summary>
    public class ViewParksMenu : CLIMenu
    {
        /// <summary>
        /// Create a list of all parks, the user will select from this
        /// </summary>
        private List<Park> parks { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parkDAO">DAO for parks table/param>
        /// <param name="campgroundDAO">DAO for campgrounds table</param>
        /// <param name="siteDAO">DAO for sites table</param>
        /// <param name="reservationDAO">DAO for reservations table</param>
        public ViewParksMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO) :
            base(parkDAO, campgroundDAO, siteDAO, reservationDAO)
        {
            this.Title = "*** Select a Park for further Details ***";
            parks = parkDAO.GetAllParks();
            for(int i = 0; i < parks.Count; i++)
            {
                this.menuOptions.Add((i+1).ToString(), parks[i].Name);
            }
            this.menuOptions.Add("Q", "Quit");
        }

        /// <summary>
        /// Handle user's menu selection
        /// </summary>
        /// <param name="choice">String representing the user's input at the menu</param>
        /// <returns>True to continue, false to exit the program</returns>
        protected override bool ExecuteSelection(string choice)
        {
            int index;
            if (menuOptions.ContainsKey(choice))
            {
                index = int.Parse(choice);
                index--;
                Park park = parks[index];
                ViewParkInformationMenu campMenu =
                    new ViewParkInformationMenu(this.MyParksDAO, this.MyCampgroundDAO, this.MySiteDAO, this.MyReservationDAO, park);
                campMenu.Run();
            }
            return true;
        }

        /// <summary>
        /// Show this before menu
        /// </summary>
        protected override void DisplayBeforeMenu()
        {
            Console.WriteLine("*** Parks Menu ***");
        }
    }
}