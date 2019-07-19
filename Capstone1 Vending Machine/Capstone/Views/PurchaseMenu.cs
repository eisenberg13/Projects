using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.IO;

namespace Capstone.Views
{
    class PurchaseMenu : CLIMenu
    {
        // Initializes a List of Strings to be used to store the categories of food purchases, for 
        // later use in displaying the appropriate message when the user finalizes transaction.
        // It's important to keep it out of the main loop for the menu, so that the menu can write to it.
        List<string> catCount = new List<string>();
        public PurchaseMenu(VendingMachine vendingMachine, MoneyManager manager) : base(vendingMachine, manager)
        {
            this.Title = "*** Purchase Menu ***";
            this.MyMoneyManager = manager;
            this.MyVendingMachine = vendingMachine;
            this.menuOptions.Add("1", "Feed Money");
            this.menuOptions.Add("2", "Select Product");
            this.menuOptions.Add("3", "Finish Transaction");
            this.menuOptions.Add("Q", "Quit");            
        }

        protected override bool ExecuteSelection(string choice)
        {            
            switch (choice)
            {
                case "1":
                    // Spawns new menu to handle adding money to balance. (PurchaseMenu.cs)
                    FeedMoney menu = new FeedMoney(MyVendingMachine, MyMoneyManager);
                    menu.Run();                        
                        return true;
                case "2":
                    // Select Product
                    Console.Write("Enter the Slot Location of the item to purchase: ");
                    string selection = Console.ReadLine().Trim().ToUpper();

                    // Loops through inventory list.
                    foreach (VendingItem item in MyVendingMachine.Inventory)
                    {
                        // Compares selection to the SlotLocation value for each of the VendingItems. 
                        if (selection == item.SlotLocation)
                        {
                            if (item.AmountInStock == 0)
                            {
                                // Notifies customer that the item is out of stock, then returns to menu.
                                Pause("Out of Stock -- Please choose another item");
                                break;
                            }
                            else
                            {
                                if(MyMoneyManager.CurrentBalance < item.Price)
                                {
                                    // Notifies user of insufficient funds and then returns to the menu.
                                    Pause("Insufficient Funds -- Please add more money");
                                    break;
                                }
                                // updates the total of money still available for purchases.
                                MyMoneyManager.UpdateBalance(item.Price);
                                // decrements the quantity of the item still in stock. 
                                item.AmountInStock--;
                                catCount.Add(item.Category);
                                // Write to the Log.txt file to keep a record of the purchase transaction 
                                using (StreamWriter sw = new StreamWriter("Log.txt", true))
                                {
                                    sw.WriteLine($"{DateTime.Now} {item.Name, -20} {item.SlotLocation, -6} {MyMoneyManager.CurrentBalance + item.Price, -10:C}{MyMoneyManager.CurrentBalance,-6:C}");
                                }
                            }
                        }
                    }
                    return true;
                case "3":
                    // Finish transaction
                    using (StreamWriter sw = new StreamWriter("Log.txt", true))
                    {
                        // this string is used to make the WriteLine look better with padding. 
                        string giveChange = "GIVE CHANGE:";
                        sw.WriteLine($"{DateTime.Now} {giveChange, -28}{MyMoneyManager.CurrentBalance, -8:C}  $0.00");
                    }
                    Console.WriteLine ($"{MyMoneyManager.ReturnChange()}");
                    // This foreach loop goes through the list "catCount" to display the "consumed" 
                    // message for the categories purchased
                    foreach (string catMessage in catCount)
                    {
                        switch (catMessage)
                        {
                            case "Chip":
                                Console.WriteLine("Crunch Crunch, Yum!");
                                break;
                            case "Candy":
                                Console.WriteLine("Munch Munch, Yum!");
                                break;
                            case "Drink":
                                Console.WriteLine("Glug Glug, Yum!");
                                break;
                            case "Gum":
                                Console.WriteLine("Chew Chew, Yum!");
                                break;
                        }
                    }
                    Pause("");
                    // Returns back to the Main Menu, from which this menu came.
                    return false;
            }
            // Continues the menu loop.
            return true;
        }
    }
}
