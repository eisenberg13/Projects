using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class VendingItem
    {
        public string SlotLocation { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }
        public string Category { get; set; }

        public VendingItem(string slotLocation, string name, decimal price, string category)
        {
            this.SlotLocation = slotLocation;
            this.Name = name;
            this.Price = price;
            this.AmountInStock = 5; // Defaults to 5 in stock. 
            this.Category = category;
        }

        public override bool Equals(object obj)
        {
            VendingItem otherItem = (VendingItem)obj;
            if (this.AmountInStock == otherItem.AmountInStock && this.Category == otherItem.Category
                && this.Name == otherItem.Name && this.Price == otherItem.Price
                && this.SlotLocation == otherItem.SlotLocation)
            {
                return true;
            }
            return false;
        }

    }
}
