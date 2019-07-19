using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Tests;
using Capstone.Views;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using System;

namespace Capstone.Tests
{
    [TestClass]
    public class DAOTests
    {
        protected string connectionString = "Server=.\\SqlExpress; Database=npcampground; Trusted_Connection=true;";
        private TransactionScope transaction;

        protected int parkId = 0;
        protected int campId = 0;
        protected int siteId = 0;
        protected int Smith = 0;
        protected int Lockhart = 0;

        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();
            string script = File.ReadAllText("TestSetup.sql");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(script, conn);

                SqlDataReader rdr = cmd.ExecuteReader();

                if(rdr.Read())
                {
                    this.parkId = Convert.ToInt32(rdr["Parkid"]);
                    this.campId = Convert.ToInt32(rdr["Campid"]);
                    this.siteId = Convert.ToInt32(rdr["Siteid"]);
                    this.Smith = Convert.ToInt32(rdr["Smith"]);
                    this.Lockhart = Convert.ToInt32(rdr["Lockhart"]);
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.transaction.Dispose();
        }

        // Helper method
        protected int GetCount(string tableName)
        {
            int rowCount = 0;
            string sql = $"SELECT COUNT(*) FROM {tableName}";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                rowCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return rowCount;
        }
    }
}