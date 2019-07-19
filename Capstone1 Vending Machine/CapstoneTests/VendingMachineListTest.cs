using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineListTest
    {
        [TestMethod]
        public void VendingMachine_ListFill_Test()
        {
            //Arrange
            List<VendingItem> fromFile = new List<VendingItem>();
            fromFile = ProductLoader.Loader();

            List<VendingItem> listItems = new List<VendingItem>();
            VendingItem item1 = new VendingItem("A1", "Potato Crisps", (decimal)3.05, "Chip");
            VendingItem item2 = new VendingItem("A2", "Stackers", (decimal)1.45, "Chip");
            VendingItem item3 = new VendingItem("A3", "Grain Waves", (decimal)2.75, "Chip");
            VendingItem item4 = new VendingItem("A4", "Cloud Popcorn", (decimal)3.65, "Chip");
            VendingItem item5 = new VendingItem("B1", "Moonpie", (decimal)1.80, "Candy");
            VendingItem item6 = new VendingItem("B2", "Cowtales", (decimal)1.50, "Candy");
            VendingItem item7 = new VendingItem("B3", "Wonka Bar", (decimal)1.50, "Candy");
            VendingItem item8 = new VendingItem("B4", "Crunchie", (decimal)1.75, "Candy");
            VendingItem item9 = new VendingItem("C1", "Cola", (decimal)1.25, "Drink");
            VendingItem item10 = new VendingItem("C2", "Dr. Salt", (decimal)1.50, "Drink");
            VendingItem item11 = new VendingItem("C3", "Mountain Melter", (decimal)1.50, "Drink");
            VendingItem item12 = new VendingItem("C4", "Heavy", (decimal)1.50, "Drink");
            VendingItem item13 = new VendingItem("D1", "U-Chews", (decimal)0.85, "Gum");
            VendingItem item14 = new VendingItem("D2", "Little League Chew", (decimal)0.95, "Gum");
            VendingItem item15 = new VendingItem("D3", "Chiclets", (decimal)0.75, "Gum");
            VendingItem item16 = new VendingItem("D4", "Triplemint", (decimal)0.75, "Gum");

            listItems.Add(item1);
            listItems.Add(item2);
            listItems.Add(item3);
            listItems.Add(item4);
            listItems.Add(item5);
            listItems.Add(item6);
            listItems.Add(item7);
            listItems.Add(item8);
            listItems.Add(item9);
            listItems.Add(item10);
            listItems.Add(item11);
            listItems.Add(item12);
            listItems.Add(item13);
            listItems.Add(item14);
            listItems.Add(item15);
            listItems.Add(item16);
            //Act
            
            //Assert
            CollectionAssert.AreEqual(listItems, fromFile, "Lists are not the same.");
        }


    }
}
