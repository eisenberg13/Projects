using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public MainMenu(VendingMachine vendingMachine, MoneyManager manager) : base(vendingMachine, manager)
        {
            this.Title = "*** Welcome to Vendo-Matic 500 ***";
            this.menuOptions.Add("1", "Display Vending Machine Items");
            this.menuOptions.Add("2", "Purchase");
            this.menuOptions.Add("Q", "Quit");
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
         
                    foreach (VendingItem item in MyVendingMachine.Inventory)
                    {
                        if (item.AmountInStock == 0)
                        {
                            Console.WriteLine($"{item.SlotLocation,-4} {item.Name,-20} {item.Price,3:C} \tQuantity: OUT OF STOCK");
                        }
                        else Console.WriteLine($"{item.SlotLocation,-4} {item.Name, -20} {item.Price, 3:C} \tQuantity: {item.AmountInStock}" );
                    }
                    Pause("");
                    return true;
                case "2":
                    PurchaseMenu menu = new PurchaseMenu(MyVendingMachine, MyMoneyManager);
                    menu.Run();
                    return true;
            }
            return true;
        }

    }
}
