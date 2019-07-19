using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    public static class ProductLoader
    {
        public static List<VendingItem> Loader()
        {
            List<VendingItem> inventory = new List<VendingItem>();
            // default path for vending machine stock list.
            string path = @"Data\vendingmachine.csv";
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // Split the line from the file into an array of strings. 
                    // fields[0] = the slot location (i.e. A1, A2, B1, etc)
                    // fields[1] = the product name
                    // fields[2] = the price (as a string)
                    // fields[3] = category (i.e. "Chip", "Candy", "Drink", or "Gum")
                    string[] fields = line.Split("|");
                    // Convert string value to decimal. 
                    decimal price = System.Convert.ToDecimal(fields[2]);
                    VendingItem item = new VendingItem(fields[0], fields[1], price, fields[3]);
                    // add newly created item to the list "inventory". 
                    inventory.Add(item);
                }
            }
            return inventory;
        }
    }
}
