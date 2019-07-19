using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkSqlDAOTests : DAOTests
    {
        [TestMethod]
        public void GetCountOfParks()
        {
            // Arrange
            IParkDAO dao = new ParkSqlDAO(connectionString);

            // Act
            IList<Park> parks = dao.GetAllParks();

            // Assert
            // Our test database has one park
            Assert.AreEqual(1, parks.Count);
        }
    }
}