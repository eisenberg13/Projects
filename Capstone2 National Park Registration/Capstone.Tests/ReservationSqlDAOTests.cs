using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;


namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlDAOTests : DAOTests
    {
        [TestMethod]
        public void MakeReservation_Test()
        {
            IReservationDAO dao = new ReservationSqlDAO(connectionString);
            int beforeCount = GetCount("reservation");
            Site site = new Site();
            site.SiteID = this.siteId;
            int id = dao.MakeReservation(site,Convert.ToDateTime("2019-06-20"),Convert.ToDateTime("2019-06-29"), "Eisenberg");


            int afterCount = GetCount("reservation");
            Assert.AreEqual(beforeCount + 1, afterCount);
            Assert.AreNotEqual(0, id);
        }


    }
}
