using Capstone.Views;
using System;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            List<VendingItem> inventory = new List<VendingItem>();
// Product loader pulls the data from the loading file. 
            inventory = ProductLoader.Loader(); 
            // Initializes "Vending Machine" and "Money Manager"
            VendingMachine vendingMachine = new VendingMachine(inventory);
            MoneyManager moneyManager = new MoneyManager();

           // Loads Main Menu to start user experience. 
            MainMenu menu = new MainMenu(vendingMachine, moneyManager);
            menu.Run();
        }
    }
}
