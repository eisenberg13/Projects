using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;


namespace Capstone.Views
{
    /// <summary>
    /// Abstract class that all our menu objects inherit from
    /// </summary>
    public abstract class CLIMenu
    {
        /*** 
         * Model Data that this menu system needs to operate on goes here.
         ***/
        public IParkDAO MyParksDAO;
        public ICampgroundDAO MyCampgroundDAO;
        public ISiteDAO MySiteDAO;
        public IReservationDAO MyReservationDAO;


        /// <summary>
        /// This is where every sub-menu puts its options for display to the user.
        /// </summary>
        protected Dictionary<string, string> menuOptions;

        /// <summary>
        /// The Title of this menu
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Labels used where they are required (printing several things at once)
        /// </summary>
        public string Labels { get; set; }

        /// <summary>
        /// Default constructor, pass in our DAO objects
        /// </summary>
        /// <param name="parkDAO">DAO for parks, created at start</param>
        /// <param name="campgroundDAO">DAO for campgrounds, created at start</param>
        /// <param name="siteDAO">DAO for campsites, created at start</param>
        /// <param name="reservationDAO">DAO for reservations, created at start</param>
        public CLIMenu(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO)
        {
            this.menuOptions = new Dictionary<string, string>();
            MyParksDAO = parkDAO;
            MyCampgroundDAO = campgroundDAO;
            MySiteDAO = siteDAO;
            MyReservationDAO = reservationDAO;
        }
        // All of our menus access all of our DAOs. It's messy but with the sequential, nested menus
        // It's less confusing than moving them around individually

        /// <summary>
        /// Run starts the menu loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                DisplayBeforeMenu();

                Console.WriteLine(this.Title);
                //Console.WriteLine(new string('=', this.Title.Length));
                Console.WriteLine("\r\nPlease make a selection:");
                Console.WriteLine(this.Labels);
                foreach (KeyValuePair<string, string> menuItem in menuOptions)
                {
                    Console.WriteLine($"{menuItem.Key} - {menuItem.Value}");
                }

                DisplayAfterMenu();

                string choice = GetString("Selection:").ToUpper().Trim();

                if (menuOptions.ContainsKey(choice))
                {
                    if (choice == "Q")
                    {
                        break;
                    }
                    if (!ExecuteSelection(choice))
                    {
                        break;
                    }
                }

            }
        }

        /// <summary>
        /// Given a valid menu selection, runs the approriate code to do what the user is asking for.
        /// </summary>
        /// <param name="choice">The menu option (key) selected by the user</param>
        /// <returns>True to keep executing the menu (loop), False to exit this menu (break)</returns>
        abstract protected bool ExecuteSelection(string choice);

        /// <summary>
        /// DisplayBeforeMenu is a virtaul mathod called after the screen is cleared and before the 
        /// menu options are displayed to the user.
        /// 
        /// Override this if you want to display your own information before the menu choices.
        /// </summary>
        virtual protected void DisplayBeforeMenu()
        {
            return;
        }

        /// <summary>
        /// DisplayAfterMenu is a virtaul mathod called after the menu options are displayed
        /// and before the user is prompted for a selection.
        /// 
        /// Override this if you want to display your own information after the menu choices.
        /// It should be no mare than 1 or 2 lines.
        /// 
        /// </summary>
        virtual protected void DisplayAfterMenu()
        {
            return;
        }
        // This is in the abstract class because it's used by a few different menus
        /// <summary>
        /// Converts a number 1-12 into its equivalent month name
        /// </summary>
        /// <param name="month">The integer value of the month 1-12</param>
        /// <returns>String of the name of the month</returns>
        protected string Month(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "Month";
            }
        }
            #region User Input Helper Methods
            /// <summary>
            /// This continually prompts the user until they enter a valid integer.
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            protected int GetInteger(string message)
        {
            int resultValue = 0;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (int.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid whole number.");
                }
            }
            return resultValue;
        }

        /// <summary>
        /// This continually prompts the user until they enter a valid double.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected double GetDouble(string message)
        {
            double resultValue = 0;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (double.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                }
            }
            return resultValue;
        }

        /// <summary>
        /// This continually prompts the user until they enter a valid bool.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool GetBool(string message)
        {
            bool resultValue = false;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (userInput.ToUpper() == "Y")
                {
                    resultValue = true;
                    break;
                }
                else if (userInput == "N")
                {
                    resultValue = false;
                    break;
                }
                else if (bool.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter [True, False, Y or N].");
                }
            }
            return resultValue;
        }

        /// <summary>
        /// This continually prompts the user until they enter a valid string (1 or more characters).
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected string GetString(string message)
        {
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                }
            }
        }

        /// <summary>
        /// Shows a message to the user and waits for the user to hit return
        /// </summary>
        /// <param name="message"></param>
        protected void Pause(string message)
        {
            Console.Write(message + " Press Enter to continue.");
            Console.ReadLine();
        }
        #endregion
    }
}