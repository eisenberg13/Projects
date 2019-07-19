using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class MoneyManager
    {
        public decimal CurrentBalance { get; private set; }


        public MoneyManager()
        {
            // Balance always starts at 0.
            this.CurrentBalance = 0.00M;
        }

        public decimal AddMoney(decimal moneyToAdd)
        {
            // Adjusts and returns the current balance based on user selection in FeedMoney menu. 
            return (this.CurrentBalance += moneyToAdd);
        }
        // Adjusts and returns balance when user makes a purchase
        public decimal UpdateBalance(decimal cost)
        {
            return (this.CurrentBalance -= cost);
        }
        // Displays message indicating change returned to user (quarters, dimes, and nickels)
        public string ReturnChange()
        {
            // Initialize variabes needed to calculate change.
            decimal quarter = 0.25M;
            decimal dime = 0.10M;
            decimal nickel = 0.05M;

            // Dividing the current balance by the value of a quarter, the int portion indicates
            // the number of quarters
            decimal numQuarters = (int)(this.CurrentBalance / quarter);
            // The math for dimes and nickels is similar, but first you must find the remainder after
            // the original balance is divided by the value of the next highest denomination of coin 
            decimal numDimes = (int)((this.CurrentBalance % quarter) / dime);
            decimal numNickel = (int)(((this.CurrentBalance % quarter) % dime) / nickel);
            // The string returned indicates the number of each coin to return. 
            string change = $"Your change is {numQuarters} quarters, {numDimes} dimes, and {numNickel} nickels.";
            // Reset current balance to 0 and return the message string. 
            this.CurrentBalance = 0;
            return change;
        }
    }
}
