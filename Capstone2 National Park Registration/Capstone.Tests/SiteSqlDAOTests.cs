using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteSqlDAOTests : DAOTests
    {
        [TestMethod]
        public void GetCountOfSites()
        {
            ISiteDAO dao = new SiteSqlDAO(connectionString);

            IList<Site> sites = dao.GetAllSites(this.campId);

            // Our test database contains one campsite
            Assert.AreEqual(1, sites.Count);
        }
        [TestMethod]
        
        public void CheckAvailableSites_Test()
        {
            ISiteDAO dao = new SiteSqlDAO(connectionString);
            Campground camp = new Campground();
            camp.CampgroundID = campId;
            camp.OpenFrom = 1;
            camp.OpenTo = 12;
            IList<Site> siteRes = dao.AvailableSites(camp, Convert.ToDateTime("2019-06-08"), Convert.ToDateTime("2019-06-12"));

            Assert.AreEqual(1, siteRes.Count);

            siteRes = dao.AvailableSites(camp, Convert.ToDateTime("2019-07-03"), Convert.ToDateTime("2019-07-08"));
            Assert.AreEqual(0, siteRes.Count);
        }
    }
}