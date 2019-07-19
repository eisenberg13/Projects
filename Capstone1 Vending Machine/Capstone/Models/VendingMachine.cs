using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        public List<VendingItem> Inventory { get; set; }

        public VendingMachine(List<VendingItem> inventory)
        {
            this.Inventory = inventory;
        }


    }
    
}
