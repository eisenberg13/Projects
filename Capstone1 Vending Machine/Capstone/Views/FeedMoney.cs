using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.IO;

namespace Capstone.Views
{
    public class FeedMoney : CLIMenu
    {
        public FeedMoney(VendingMachine vendingMachine, MoneyManager manager) : base(vendingMachine, manager)
        {
            this.Title = "*** Feed Money Into Machine ***";
            this.MyMoneyManager = manager;
            this.MyVendingMachine = vendingMachine;
            // Only valid US denominations between $1 and $20 are allowed. 
            this.menuOptions.Add("1", "One Dollar Bill");
            this.menuOptions.Add("2", "Two Dollar Bill");
            this.menuOptions.Add("5", "Five Dollar Bill");
            this.menuOptions.Add("10", "Ten Dollar Bill");
            this.menuOptions.Add("20", "Twenty Dollar Bill");
            this.menuOptions.Add("Q", "Quit");
        }

        protected override bool ExecuteSelection(string choice)
        {

            switch (choice)
            {
                // Using cases in a switch statement limits the user's input to valid denominations 
                // of bills, returning the user to the menu if invalid input is entered.
                case "1":
                    int money = 1;
                    MyMoneyManager.AddMoney(money);
                    // Adding $1 is logged to Log.txt
                    using (StreamWriter sw = new StreamWriter("Log.txt", true))
                    {
                        string feedMoney = "FEED MONEY:";
                        sw.WriteLine($"{DateTime.Now} {feedMoney,-27} {money,-10:C}{MyMoneyManager.CurrentBalance,-6:C}");
                    }
                    // Returns to purchase menu.
                    return false;
                case "2":
                    money = 2;
                    MyMoneyManager.AddMoney(money);
                    // Writes to Log.txt
                    using (StreamWriter sw = new StreamWriter("Log.txt", true))
                    {
                        string feedMoney = "FEED MONEY:";
                        sw.WriteLine($"{DateTime.Now} {feedMoney,-27} {money,-10:C}{MyMoneyManager.CurrentBalance,-6:C}");
                    }
                    // Returns to purchase menu.
                    return false;
                case "5":
                    money = 5;
                    MyMoneyManager.AddMoney(money);
                    using (StreamWriter sw = new StreamWriter("Log.txt", true))
                    {
                        string feedMoney = "FEED MONEY:";
                        sw.WriteLine($"{DateTime.Now} {feedMoney,-27} {money,-10:C}{MyMoneyManager.CurrentBalance,-6:C}");
                    }
                    return false;
                case "10":
                    money = 10;
                    MyMoneyManager.AddMoney(money);
                    using (StreamWriter sw = new StreamWriter("Log.txt", true))
                    {
                        string feedMoney = "FEED MONEY:";
                        sw.WriteLine($"{DateTime.Now} {feedMoney,-27} {money,-10:C}{MyMoneyManager.CurrentBalance,-6:C}");
                    }
                    return false;
                case "20":
                    money = 20;
                    MyMoneyManager.AddMoney(money);
                    using (StreamWriter sw = new StreamWriter("Log.txt", true))
                    {
                        string feedMoney = "FEED MONEY:";
                        sw.WriteLine($"{DateTime.Now} {feedMoney,-27} {money,-10:C}{MyMoneyManager.CurrentBalance,-6:C}");
                    }

                    return false;
            }
            // Loops back to FeedMoney menu if invalid input is received. 
            return true;
        }
    }
}
