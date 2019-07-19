using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class MoneyManagerTests
    {

        [DataTestMethod]
        [DataRow(20)]
        [DataRow(5)]
        public void AddMoneyTest(int moneyToAdd)
        {
            // Arrange
            MoneyManager manager = new MoneyManager();
            decimal output = (decimal)moneyToAdd;
            // Act
            decimal result = manager.AddMoney(moneyToAdd);
            // Assert
            Assert.AreEqual(output, result, "The expected balance should equal the money added.");
        }

        [DataTestMethod]
        [DataRow(1.50, 8.50)]
        public void UpdateBalanceTest(double cost, double balanceExpected)
        {
            // Arrange
            MoneyManager manager = new MoneyManager();
            manager.AddMoney(10.00M);
            // Act
            decimal result = manager.UpdateBalance((decimal)cost);
            // Assert
            Assert.AreEqual((decimal)balanceExpected, result, "The result did not equal the expected balance.");

        }

        [DataTestMethod]
        [DataRow(0.95, "Your change is 3 quarters, 2 dimes, and 0 nickels.")]
        [DataRow(1.40, "Your change is 5 quarters, 1 dimes, and 1 nickels.")]
        [DataRow(0.55, "Your change is 2 quarters, 0 dimes, and 1 nickels.")]
        [DataRow(0.15, "Your change is 0 quarters, 1 dimes, and 1 nickels.")]

        public void ReturnChangeTest(double balance, string expectedOutput)
        {
            //Arrange
            MoneyManager moneyManager = new MoneyManager();
            moneyManager.AddMoney((decimal)balance);
            //Act
            string output = moneyManager.ReturnChange();
            //Assert
            Assert.AreEqual(expectedOutput, output, "The output was incorrect");

        }

    }
}
