using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundSqlDAOTests : DAOTests
    {
        [TestMethod]
        public void GetCountOfSites()
        {
            // Arrange
            ISiteDAO dao = new SiteSqlDAO(connectionString);

            // Act
            IList<Site> sites = dao.GetAllSites(campId);

            // Assert
            // Our test database has one site
            Assert.AreEqual(1, sites.Count);
        }
    }
}