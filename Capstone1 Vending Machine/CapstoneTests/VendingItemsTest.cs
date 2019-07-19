using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CapstoneTests
{
    [TestClass]
    public class VendingItemsTest
    {
        [DataTestMethod]
        [DataRow("A1", "Crispy Crisps", 2.50, "Chip")]
        [DataRow("B3", "Wonka Bar", 1.50, "Candy")]
        [DataRow("D2", "Little League", 0.95, "Gum")]
        public void VendingItemsConstructorTest(string slotLocation, string name, double cost, string category)
        {
            // Arrange
            VendingItem item = new VendingItem(slotLocation, name, (decimal)cost, category);
            // Act

            // Assert
            Assert.AreEqual(slotLocation, item.SlotLocation, "The slot location did not set properly.");
            Assert.AreEqual(name, item.Name, "The name did not set properly.");
            Assert.AreEqual((decimal)cost, item.Price, "The price did not set properly.");
            Assert.AreEqual(category, item.Category, "The category did not set properly.");
            Assert.AreEqual(5, item.AmountInStock, "The slot location did not set properly.");

        }
    }
}
